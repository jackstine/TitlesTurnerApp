using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TitlesDatabase;
using TitlesDatabase.Models;
using TitlesDatabase.Repos.Compositions;
using TitlesLogic.Repos;

namespace TitlesTurner.Controllers
{
    public class TitlesController : ApiController
    {
        public IEnumerable<dynamic> Get(String name)
        {
            return TitleOtherNamesLogic.GetByName(name).Select(t => new
            {
                TitleName = t.TitleName,
                TitleId = t.TitleId,
                ReleaseYear = t.ReleaseYear,
                ProcessedDateTimeUTC = t.ProcessedDateTimeUTC,
                TitleTypeId = t.TitleTypeId,
                TitleNameSortable = t.TitleNameSortable,
            });
        }

        public TitleComposite Get(int id)
        {
            return new TitleCompositeRepo().getById(id);
        }

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //[HttpGet]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}