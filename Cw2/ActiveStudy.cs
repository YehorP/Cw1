using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Cw2
{
    public class ActiveStudy:IEquatable<ActiveStudy>
    {
        public ActiveStudy()
        {
        }

        public ActiveStudy(string name)
        {
            this.name = name;
            this.numberOfStudents = 0;
        }

        public string name { get; set; }
        public int numberOfStudents { get; set; }

        public bool Equals([AllowNull] ActiveStudy other)
        {
            return this.name==other.name;
        }
    }
}
