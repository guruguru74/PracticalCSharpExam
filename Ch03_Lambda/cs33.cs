using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03_Lambda
{
    internal class cs33
    {
        private List<string> list = new List<string>
        {
            "Seoul", "New Delhi", "Bangkok", "London", "Paris", 
            "Berlin", "Canberra", "Hong Kong",
        };

        public void Do()
        {
            var exists = list.Exists(s => s[0] == 'A');
            Console.WriteLine($"exist => {exists}");

            var name = list.Find(s => s.Length == 6);
            Console.WriteLine($"Find => {name}");

            int index = list.FindIndex(s => s == "Berlin");
            Console.WriteLine($"FindIndex => {index}");

            var names = list.FindAll(s => s.Length <= 5);
            foreach(var s in names)
                Console.WriteLine(s);

            Console.WriteLine("==============================");
            list.ForEach(s => Console.WriteLine(s));
            Console.WriteLine("==============================");

            var removedCount = list.RemoveAll(s => s.Contains("on"));
            Console.WriteLine($"RemoveAll => {removedCount}");

            Console.WriteLine("==============================");
            list.ForEach(Console.WriteLine);
            Console.WriteLine("==============================");
        }
    }
}
