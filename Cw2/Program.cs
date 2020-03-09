using System;
using System.IO;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"D:\Cw1\Cw2\dane.csv";

            /*var lines = File.ReadLines(path);

            foreach (var line in lines)
            {
                Console.WriteLine(line.ToString());
            }

            var parsedDate = DateTime.Parse("2020-03-9");
            Console.WriteLine(parsedDate);
            var now = DateTime.Now;
            Console.WriteLine(now);
            var today = DateTime.Today;
            Console.WriteLine(today.ToShortDateString());*/
            Uczelnia ucz = new Uczelnia(File.ReadLines(path));
        }
    }
}
