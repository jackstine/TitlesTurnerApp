using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitlesDatabase.Repos.Abstraction
{
    public interface RepoI<K, T> where T : class where K : DbContext
    {
        IQueryable<T> Get();
        IQueryable<T> GetLimit(int start, int end);
        int Count();
    }
}
