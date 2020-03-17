﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;

namespace Cw3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{IdStudent=1,FirstName="Jan",LastName="Kowalski"},
                new Student{IdStudent=1,FirstName="John",LastName="Malinowski"},
                new Student{IdStudent=1,FirstName="Bill",LastName="Andrzejewski"}
            };
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
