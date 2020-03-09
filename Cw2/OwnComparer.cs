using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    class OwnComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals($"{x.fname} {x.lname} {x.studentNumber}", $"{y.fname} {y.lname} {y.studentNumber}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{obj.fname} {obj.lname} {obj.studentNumber}");
        }
    }
}
