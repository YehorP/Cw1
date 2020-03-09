using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    class Uczelnia
    {
        public DateTime createdAt = DateTime.Today;
        public string author = "Yehor Pakhomov";
        public HashSet<Student> studenci = new HashSet<Student>(new OwnComparer());

        public Uczelnia(IEnumerable<string> lines)
        {
            string[] cutData;
           foreach(var line in lines)
            {
                cutData = line.Split(",");
                if (cutData.Length == 9)
                {
                    studenci.Add(new Student(cutData));
                }
            }

            Console.WriteLine(studenci.ToString());
        }
    }
}
