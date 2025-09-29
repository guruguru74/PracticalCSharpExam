using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ch14._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sample = new SampleCode();
            sample.DownloadString();

            sample.DownloadFile();

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }

    class SampleCode
    {
        public void DownloadString()
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            var html = wc.DownloadString("https://www.visualstudio.com/");
            Console.WriteLine(html);
        }

        public void DownloadFile()
        {
            var wc = new WebClient();
            var url = "https://www.visualstudio.com/";
            var fileName = @"example.zip";
            wc.DownloadFile(url, fileName);
        }

        // 이 메서드는 환경을 만들지 않으면 실행할 수 없습니다.
        // 지정한 URL이 존재하지 않으므로 예외가 발생합니다.
        // 따라서 이 콘솔 프로그램을 실행할 때 이 함수는 호출되지 않도록 했습니다.
        //[ListNo("List 14-17")]
        private void DownloadFileAsync()
        {
            var wc = new WebClient();
            var url = new Uri("http://localhost/example.zip");
            var filename = @"D:\temp\example.zip";
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            wc.DownloadFileAsync(url, filename);
        }

        static void wc_DownloadProgressChanged(object sender,
                            DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine("{0}% {0}/{1}", e.ProgressPercentage,
                              e.BytesReceived, e.TotalBytesToReceive);
        }

        static void wc_DownloadFileCompleted(object sender,
                            System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine("내려받기가 끝났습니다.");
        }
    }
}
