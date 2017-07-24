using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitlesDatabase.Repos.Abstraction;

namespace TitlesDatabase.Repos
{
    public class TitleRepo : RepoAbstract<TitlesEntities, Title>
    {
        public IQueryable<Title> getById(int Id)
        {
            return Get().Where(t => t.TitleId == Id);
        }

        public static dynamic getTitle(Title t)
        {
            return new
            {
                TitleId = t.TitleId,
                ReleaseYear = t.ReleaseYear,
                TitleName = t.TitleName,
                ProcessedDateTimeUTC = t.ProcessedDateTimeUTC,
                TitleNameSortable = t.TitleNameSortable,
                TitleTypeId = t.TitleTypeId
            };
        }
    }
}
