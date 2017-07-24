using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitlesDatabase;
using TitlesDatabase.Repos;
using TitlesDatabase.Repos.Compositions;

namespace TitlesLogic.Repos
{
    public static class TitleOtherNamesLogic
    {
        public static IEnumerable<Title> GetByName(String name)
        {
            TitlesOtherNamesRepo repo = new TitlesOtherNamesRepo();
            var titles = repo.GetByName(name).ToList();
            return U_A.getUnique(titles, t => t.TitleId);
        }
    }
}
