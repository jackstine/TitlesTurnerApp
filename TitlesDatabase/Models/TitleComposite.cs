using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitlesDatabase.Models
{
    /// <summary>
    /// Given a Title, this will contain all the information for the Title
    /// </summary>
    public class TitleComposite
    {
        public Title Title { get; set; }
        public List<OtherName> OtherNames { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Award> Awards { get; set; }
        public List<TitleParticipant> TitleParticipants { get; set; }
        public List<Participant> Participants { get; set; }
        public List<TitleParticipantInfoModel> TitleParticipantInfo { get; set; }
        public List<StoryLine> StoryLines { get; set; }
    }
}
