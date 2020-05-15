using Cw9.DTOs.Requests;
using Cw9.DTOs.Responses;
using Cw9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw9.Services
{
    public interface IStudentsDbService
    {
        IEnumerable<Student> GetStudents();
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest req);
        PromoteStudentResponse PromoteStudent(PromoteStudentRequest req);
        bool IsStudentExists(String StudentIndexNumber);

        bool DeleteStudent(String StudentIndexNumber);
        bool UpdateStudent(UpdateStudentRequest request);
    }
}
