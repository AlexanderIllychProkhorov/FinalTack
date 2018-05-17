using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTasck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            try
            {
                Console.WriteLine("Please input path to the directory");
                var path = Console.ReadLine();
                DirectoryInfo directoryList = new DirectoryInfo(path);
                List<DirectoryInfo> list = new List<DirectoryInfo>();
                list.Add(directoryList);
                var directoryList1 = Directory.GetDirectories(path);
                if (directoryList != null)
                {
                 //   foreach (string dir in directoryList)

                    //    Console.WriteLine(dir);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Directory List is null.Please input correct path");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
