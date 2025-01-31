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


        public void Do()
        {
            var numbers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };
            //Judgment judge = IsEven;
            //var count = Count(numbers, judge);

            //var count = Count(numbers, IsEven);

            // 익명 메서드 이용.
            //var count = Count(numbers, delegate (int n) { return n % 2 == 0; });

            // Lambda - 홀수의 갯수를 셈
            var count = Count(numbers, n => n % 2 == 1);
            Console.WriteLine($"Lambda - 홀수의 갯수를 셈. => {count}");

            // Lambda - 5 이상인 수의 갯수를 셈.
            count = Count(numbers, n => n >= 5);
            Console.WriteLine($"Lambda - 5 이상인 수의 갯수를 셈. => {count}");

            // Lambda - '1'이 포함된 수의 갯수를 셈.
            count = Count(numbers, n => n.ToString().Contains('1'));
            Console.WriteLine($"Lambda - '1'이 포함된 수의 갯수를 셈. => {count}");
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

        public bool IsEven(int n)
        {
            return n % 2 == 0;
        }


    }
}
