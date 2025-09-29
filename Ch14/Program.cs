using CSharpPhrase.CustomSection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Ch14
{
    internal class Program
    {
        private readonly string STR_LINE = "===========================================";

        static void Main(string[] args)
        {
            //RunNotepad();

            //RunAndWaitNotepad();

            //RunAndWaitNotepad2();

            //RunNotepadwithProcessInfo();

            //RunVerb();


            //GetThisVersion();

            //GetFileVersion();

            //GetOSVersion();


            GetAppSettings();

            GetAllSettings();

            GetCustomSection();


            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        #region Process execute
        private static void RunNotepad()
        {
            var path = @"%SystemRoot%\system32\notepad.exe";
            var fullpath = Environment.ExpandEnvironmentVariables(path);
            System.Diagnostics.Process.Start(fullpath);
        }

        private static int RunAndWaitNotepad()
        {
            var path = @"%SystemRoot%\system32\notepad.exe";
            var fullpath = Environment.ExpandEnvironmentVariables(path);
            using (var process = System.Diagnostics.Process.Start(fullpath))
            {
                if (process.WaitForExit(10000))
                    return process.ExitCode;
                throw new TimeoutException();
            }
        }

        private static void RunAndWaitNotepad2()
        {
            var path = @"%SystemRoot%\system32\notepad.exe";
            var fullpath = Environment.ExpandEnvironmentVariables(path);
            var process = System.Diagnostics.Process.Start(fullpath);
            process.EnableRaisingEvents = true;
            process.Exited += (sender, eventArgs) =>
            {
                //this.Invoke((Action) delegate
                //{
                Console.WriteLine("Process exited");
                //});
            };
        }

        private static void RunNotepadwithProcessInfo()
        {
            var path = @"%SystemRoot%\system32\notepad.exe";
            var fullpath = Environment.ExpandEnvironmentVariables(path);

            var startInfo = new System.Diagnostics.ProcessStartInfo(fullpath)
            {
                Arguments = @"C:\Temp\test.txt",
                WorkingDirectory = @"C:\Temp",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized
            };

            System.Diagnostics.Process.Start(startInfo);
        }

        private static void RunVerb()
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = @"c:\Windows\Media\chimes.wav",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                Verb = "play",
            };

            Process.Start(startInfo);
        }
        #endregion

        #region GetVersion
        private static void GetThisVersion()
        {
            var asm = Assembly.GetExecutingAssembly();
            var ver = asm.GetName().Version;
            Console.WriteLine("===========================================");
            Console.WriteLine("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
            Console.WriteLine("===========================================");
        }

        private static void GetFileVersion()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var ver = FileVersionInfo.GetVersionInfo(location);
            Console.WriteLine("===========================================");
            Console.WriteLine("{0}.{1}.{2}.{3}",
                ver.FileMajorPart, ver.FileMinorPart, ver.FileBuildPart, ver.FilePrivatePart);
            Console.WriteLine("===========================================");
        }

        private static void GetOSVersion()
        {
            var path = @"%SystemRoot%\system32\notepad.exe";
            var fullpath = Environment.ExpandEnvironmentVariables(path);
            var verInfo = FileVersionInfo.GetVersionInfo(fullpath);
            Console.WriteLine("===========================================");
            Console.WriteLine("Product: {0}", verInfo.ProductName);
            Console.WriteLine("File: {0}", verInfo.FileName);
            Console.WriteLine("Version: {0}", verInfo.FileVersion);
            Console.WriteLine("Product Version: {0}", verInfo.ProductVersion);
            Console.WriteLine("===========================================");
        }
        #endregion

        private static void GetAppSettings()
        {
            var enableTraceStr = ConfigurationManager.AppSettings["EnableTrace"];
            var enableTrace = bool.Parse(enableTraceStr);
            var timeoutStr = ConfigurationManager.AppSettings["Timeout"];
            int timeout = int.Parse(timeoutStr);
        }

        private static void GetAllSettings()
        {
            var settings = ConfigurationManager.AppSettings;
            foreach (var key in settings.AllKeys)
            {
                Console.WriteLine("{0} : {1}", key, settings[key]);
            }
        }


        //private static void GetCustomSection()
        //{
        //    var cs = ConfigurationManager.GetSection("myAppSettings") as MyAppSettings;
        //    var option = cs.TraceOption;

        //    Console.WriteLine("Enabled: {0}", option.Enabled);
        //    Console.WriteLine("FilePath: {0}", option.FilePath);
        //    Console.WriteLine("BufferSize: {0}", option.BufferSize);
        //}

        public static void GetCustomSection()
        {
            var cs = ConfigurationManager.GetSection("myAppSettings") as MyAppSettings;
            var option = cs.TraceOption;

            Console.WriteLine(option.BufferSize);
            Console.WriteLine(option.Enabled);
            Console.WriteLine(option.FilePath);
        }
    }
}
