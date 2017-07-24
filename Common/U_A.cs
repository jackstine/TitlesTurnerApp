using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// A list of Algorithms
    /// </summary>
    public static class U_A
    {
        public static IEnumerable<T> getUnique<T,K>(IEnumerable<T> list, Func<T,K> keyFunc)
        {
            Dictionary<K, T> dict = new Dictionary<K, T>();
            foreach (T t in list)
            {
                K key = keyFunc(t);
                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, t);
                }
            }
            return dict.Values;
        }
    }
}
