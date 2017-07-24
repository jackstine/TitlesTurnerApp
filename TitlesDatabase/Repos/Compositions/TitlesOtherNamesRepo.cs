using System;
using System.Data.Entity;
using System.Linq;
using TitlesDatabase.Models;
using TitlesDatabase.Repos.Abstraction;

namespace TitlesDatabase.Repos.Compositions
{
    public class TitlesOtherNamesRepo : RepoAbstract<TitlesEntities, Title>
    {

        public IQueryable<Title> GetByName(String name)
        {
            var lower = name.ToLower();
            return Get().Where(t => t.OtherNames.Select(o => o.TitleNameSortable.ToLower()).Contains(lower) || t.OtherNames.Select(o => o.TitleName.ToLower()).Contains(lower) ||
                t.TitleName.ToLower().Contains(name) || t.TitleNameSortable.ToLower().Contains(name));
        }

        protected override IQueryable<Title> Include(DbSet<Title> o)
        {
            return o.Include(t => t.OtherNames);
        }
    }
}
