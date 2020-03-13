using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cw2
{
    public class Uczelnia
    {
        [XmlAttribute(attributeName: "createdAt")]
        [JsonPropertyName("createdAt")]
        public string createdAt { get; set; }
        [JsonPropertyName("author")]
        [XmlAttribute(attributeName: "author")]
        public string author { get; set; }
        public HashSet<Student> studenci { get; set; }
        public List<ActiveStudy> activeStudies { get; set; }

        private StreamWriter fs;
        public Uczelnia()
        {
        }

        public Uczelnia(IEnumerable<string> lines)
        {
            this.createdAt = DateTime.Today.ToShortDateString();
            this.author = "Yehor Pakhomov";
            this.studenci = new HashSet<Student>(new OwnComparer());
            this.activeStudies = new List<ActiveStudy>();
            this.fs = new StreamWriter(@"log.txt");
            string[] cutData;
            Student tmpStudent;
            int index;
            ActiveStudy tmpStudy;
            List<String> tmpList;
           foreach(var line in lines)
            {
                cutData = line.Split(",");
                tmpList = new List<string>(cutData);
                if (cutData.Length == 9 && !tmpList.Contains("") && !tmpList.Contains(" "))
                {
                    tmpStudent = new Student(cutData);
                    
                    if (studenci.Add(tmpStudent))
                    {
                        tmpStudy = new ActiveStudy(tmpStudent.studies.name);
                        index = activeStudies.IndexOf(tmpStudy);
                        if (index == -1)
                        {
                            tmpStudy.numberOfStudents += 1;
                            activeStudies.Add(tmpStudy);
                        }
                        else
                        {
                            activeStudies[index].numberOfStudents += 1;
                        }
                    }
                }
                else {
                    fs.Write(line+" (is a wrong record)"+ System.Environment.NewLine);
                    fs.Flush();
                }
            }
            fs.Close();
        }
    }
}
