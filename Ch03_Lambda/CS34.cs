using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03_Lambda
{
    internal class CS34
    {
        private List<string> names = new List<string>
        {
            "Seoul", "New Delhi", "Bangkok", "London", "Paris",
            "Berlin", "Canberra", "Hong Kong",
        };

        private string[] arrNames =
        {
            "Seoul", "New Delhi", "Bangkok", "London", "Paris",
            "Berlin", "Canberra", "Hong Kong",
        };

        public void Do()
        {
            IEnumerable<string> query = names.Where(s => s.Length <= 6);

            foreach (string s in query)
                Console.WriteLine(s);

            var query2 = names.Select(s => s.Length);
            foreach (var n in query2)
                Console.Write("{0} ", n);
            Console.WriteLine("");


            //// 지연 실행
            //var query3 = arrNames.Where(s => s.Length <= 5);
            //foreach (var item in query3)
            //    Console.WriteLine(item);
            //Console.WriteLine("------");

            //arrNames[0] = "Busan";
            //foreach (var item in query3)    // 실제 값에 접근할 때 쿼리가 실행됨.
            //    Console.WriteLine(item);

            // 즉시 실행
            var query4 = arrNames.Where(s => s.Length <= 5).ToArray();
            foreach (var item in query4)
                Console.WriteLine(item);
            Console.WriteLine("------");

            names[0] = "Busan";
            foreach(var item in query4)
                Console.WriteLine(item);

            var count = arrNames.Count(s => s.Length > 5);
            Console.WriteLine(count);

            /*
            쿼리 구문 vs 메서드 구문
            1. 쿼리 구분은 LINQ의 모든 기능을 이용할 수 없다.
            2. 점(.)으로 연결하는 메서드 구분이 생각을 방해받지 않고 연속해서 코드를 작성할 수 있다.
            3. 점으로 연결하는 메서드 구문은 비주얼 스튜디오가 가진 강력한 인텔리센스 기능을 충분히 활용할 수 있다.
            */

        }
    }
}
