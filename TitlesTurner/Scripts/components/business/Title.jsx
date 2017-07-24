class TitleApis{
    getByName(name, cb) {
        if (name && name != "") {
            var that = this;
            fetch('api/Titles?name=' + name)
                .then(response => response.json())
                .then(cb);
        }
    }

    getTitle(id, cb) {
        if (id) {
            var that = this;
            fetch('api/Titles?id=' + id)
                .then(response => response.json())
                .then(cb);
        }
    }
}

class Titles extends React.Component {
    constructor(props) {
        super(props);
        this.api = new TitleApis();
        this.getTitles = this.getTitles.bind(this);
        this.selector = this.selector.bind(this);
        this.goBackToList = this.goBackToList.bind(this);
        this.state = { titles: [], selectedID: null, showTitle: false };
    }

    getTitles(name) {
        var that = this;
        this.api.getByName(name, titles => {
            that.setState({ titles: titles });
            console.log(titles);
        });
    }

    selector(id) {
        this.setState({ selectedID: id, showTitle: true });
    }

    goBackToList() {
        this.setState({ showTitle: false });
    }

    render() {
        var app = null;
        if (this.state.titles.length > 0) {
            var titles = <TitleTable titles={this.state.titles} onSelected={this.selector}/>;
        }
        if (this.state.showTitle) {
            app = <Title id={this.state.selectedID} onBack={this.goBackToList} />;
        }
        else {
            app = <div><InputButton callFunction={this.getTitles} text="Search Titles by Name" /> { titles }</div>;
        }
        return (app);
    }

}

class TitleTable extends React.Component {
    constructor(props) {
        //onSelected - this is the method that returns the Id of the Title
        super(props);
        this.selectedTitle = this.selectedTitle.bind(this);
    }

    selectedTitle(model) {
        this.props.onSelected(model.TitleId);
    }

    render() {
        var titleFields = [{ field: "TitleName", onClick: this.selectedTitle }, "TitleNameSortable", "ReleaseYear"];
        return <Table models={this.props.titles} fields={titleFields}/>;
    }
}

class TitleElement extends React.Component {
    constructor(props) {
        //value - this is the title
        //onClick - this is an action on the TitleName, when it is clicked
        super(props);
    }

    render() {
        var t = this.props.value;
        return (<div>{t.TitleName} {t.TitleNameSortable} {t.ReleaseYear}</div>);
    }
}

class InputButton extends React.Component {
    constructor(props) {
        //callFunction(value) - this is the function called on button click, the value is the input
        //text - this is the text from the button
        super(props);
        this.onInputChange = this.onInputChange.bind(this);
        this.buttonClick = this.buttonClick.bind(this);
        this.state = { value: '' };
    }

    buttonClick() {
        this.props.callFunction(this.state.value);
    }

    onInputChange(val) {
        this.setState({ value: val });
    }

    render() {
        var value = this.state.value;
        return (<div>
            <button onClick={this.buttonClick}>{this.props.text}</button>
            <Input onChange={this.onInputChange} value={value} />
                </div>);
    }
}

class Input extends React.Component {
    constructor(props) {
        //onChange - this is the function called when the input changes
        super(props);
    }

    render() {
        return (<input type='text' onChange={e => this.props.onChange(e.target.value)}></input>);
    }
}




class Title extends React.Component {
    constructor(props) {
        //id - this is the id of the Title to view
        //onBack - this is the function that gets called when you go back
        super(props);
        this.api = new TitleApis();
        this.state = { title: null };
    }

    componentDidMount() {
        var that = this;
        this.api.getTitle(this.props.id, function (title) {
            that.setState({ title: title });
        });
    }

    render() {
        var t = this.state.title;
        if (t) {
            var awardFields = ["AwardYear", "AwardWon", "Award1", "AwardCompany"];
            var genresFields = ["Name"];
            var participantFields = ["participant.Name", "participant.ParticipantType", "titleParticipant.IsKey", "titleParticipant.RoleType", "titleParticipant.IsOnScreen"];
            return (<div>
                <button onClick={this.props.onBack}>Go Back To Search</button>
                <Table header={"Awards"} fields={awardFields} models={t.Awards} />
                <Table header={"Genres"} fields={genresFields} models={t.Genres} />
                <Table header={"Participants"} fields={participantFields} models={t.TitleParticipantInfo} />
                    </div>);
        }
        return (null);
    }
}


class Table extends React.Component {
    constructor(props) {
        //header - this is the header for the table
        //models - this is the model of the table
        //fields - these are the fields of the model, that are shown in the table
        super(props);
        this.tf = new TableField();
    }

    render() {
        var that = this;
        var fields = this.props.fields;
        var models = this.props.models;
        var headers = fields.map((f, i) => that.tf.getHeader(f, i));
        var head = <thead><tr>{headers}</tr></thead>;
        var rows = [];
        models.forEach((mv, mi) =>
            rows.push(that.tf.getFieldValue(mv, fields, rows.length))
        );
        var body = <tbody>{rows}</tbody>;
        return <span><h1>{this.props.header}</h1><table className="table">{head}{body}</table></span>;
    }
}

class TableField {
    getHeader(field, i) {
        var f = field;
        if (field.field) {
            f = field.field;
        }
        var lastOne = f;
        var fs = f.split(".").forEach(v => lastOne = v);
        return <th key={i}>{lastOne}</th>;
    }

    getFieldValue(model, fields, i) {
        var mv = model;
        var that = this;
        return (
            <tr key={i}>{fields.map((f, i) => that.getColumnValue(mv, f, i))}</tr>
        );
    }

    getColumnValue(mv, field, i) {
        var f = field;
        if (field.field) {
            f = field.field;
        }
        var lastOne = mv;
        f.split(".").forEach(v => lastOne = lastOne[v]);
        var columnValue = <td key={i}>{lastOne}</td>;
        if (field.onClick) {
            columnValue = <td key={i} onClick={e => field.onClick(mv)}>{lastOne}</td>;
        }
        return columnValue;
    }
}

class H extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
            <h1>{this.props.text}</h1>
                {this.props.children}
            </div>);
    }
}

ReactDOM.render(
    <Titles />,
    document.getElementById('content')
);