using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitlesLogic.Repos;

namespace TitlesLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            var titles = TitleOtherNamesLogic.GetByName("be");
            foreach(var t in titles)
            {
                Console.WriteLine(t.TitleId + " " + t.TitleName);
            }
            Console.ReadLine();
        }
    }
}
