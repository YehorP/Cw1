using Cw5.DTOs.Requests;
using Cw5.DTOs.Responses;
using Cw5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw5.Services
{
    public interface IStudentsDbService
    {
        IEnumerable<Student> GetStudents();

        EnrollStudentResponse EnrollStudent(EnrollStudentRequest req);
        void PromoteStudent();

    }
}
