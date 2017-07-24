using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitlesDatabase.Repos.Abstraction
{
    public abstract class RepoAbstract<K,T> : RepoI<K,T> where T :class where K : DbContext, new()
    {

        public RepoAbstract() {}

        public int Count()
        {
            return Get().Count();
        }

        public virtual IQueryable<T> Get()
        {
            return Include(new K().Set<T>());
        }

        protected virtual IQueryable<T> Include(DbSet<T> o)
        {
            return o;
        }

        public IQueryable<T> GetLimit(int start, int end)
        {
            if (start > end)
            {
                throw new Exception("For GetLimit, you cannot have a start larger than an end");
            }
            return Get().Skip(start).Take(end - start + 1);
        }
    }
}
