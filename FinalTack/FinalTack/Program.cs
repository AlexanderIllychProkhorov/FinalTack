using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalTasck
{
    class Program
    {
        public struct MyThreadArg
        {
            public MyThreadArg(string path) { Path = path; Mtx = new Mutex(); }
            public string Path;
            public Mutex Mtx;
        }
        static void MyThreadFunc(object arg)
        {
            var threadArgs = ((MyThreadArg)arg);

            var directoryList = new DirectoryInfo(threadArgs.Path);
            Console.WriteLine("List of folders: ");
            foreach (var dir in directoryList.GetDirectories())
            {
                {
                    Console.WriteLine(dir.Name);
                    Console.WriteLine("List of subfolders: ");
                    foreach (var sdir in dir.GetDirectories())
                    {
                        threadArgs.Mtx.WaitOne();
                        Thread.Sleep(100);
                        Console.WriteLine(sdir.Name);
                        Console.WriteLine();
                        threadArgs.Mtx.ReleaseMutex();
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            var subdir = string.Empty;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("Please input path to the directory");
            var path = Console.ReadLine();
            Thread myThread = new Thread(MyThreadFunc);
            var threaedArgs = new MyThreadArg(path);
            myThread.Start(threaedArgs);
            while (myThread.ThreadState != ThreadState.Stopped)
            {
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    threaedArgs.Mtx.WaitOne();
                    while (true)
                    {
                        if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                        {
                            threaedArgs.Mtx.ReleaseMutex();
                            break;
                        }
                    }
                }
            }
            myThread.Join();

        }
    }
}
