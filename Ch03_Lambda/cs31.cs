using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03_Lambda
{
    internal class cs31
    {
        public int Count(int num)
        {
            var numbers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };
            int count = 0;
            foreach (var n in numbers)
            {
                if (n == num)
                    count++;
            }

            return count;
        }

        public delegate bool Judgment(int value);
        public int Count(int[] numbers, Judgment judge)
        {
            int count = 0;
            foreach (var n in numbers)
            {
                if (judge(n) == true)
                    count++;
            }
            return count;
        }
    }
}
