using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {



            }
            else
            {
                var defaultpath = @"W:\Cw1\Cw2\dane.csv";
                var result = @".\..\..\..\result.xml";
                Uczelnia ucz = new Uczelnia(File.ReadLines(defaultpath));
                using (FileStream fs = new FileStream(result, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));
                    serializer.Serialize(fs, ucz);
                }
            }
            //var dest2 = @".\..\..\..\res2.json";

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



            //File.WriteAllText(dest2, JsonSerializer.Serialize<Uczelnia>(ucz));
            //Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
}
