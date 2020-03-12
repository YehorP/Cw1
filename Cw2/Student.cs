using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cw2
{
    public class Student
    {
        public Student()
        {
        }

        public Student(string[] separatedLines)
        {
            this.fname = separatedLines[0];
            this.lname = separatedLines[1];
            this.studies = new Studies { name = separatedLines[2], mode = separatedLines[3] };
            this.studentNumber = "s" + separatedLines[4];
            this.birthDate = DateTime.Parse(separatedLines[5]).ToShortDateString();
            this.email = separatedLines[6];
            this.mothersName = separatedLines[7];
            this.fathersName = separatedLines[8];
        }
        [XmlAttribute(attributeName: "indexnumber")]
        [JsonPropertyName("indexnumber")]
        public string studentNumber { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string birthDate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }
        public Studies studies{ get; set; }

       
    }
}
