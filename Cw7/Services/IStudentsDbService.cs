using Cw7.DTOs.Requests;
using Cw7.DTOs.Responses;
using Cw7.Models;
using Cw7.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public interface IStudentsDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest req);
        PromoteStudentResponse PromoteStudent(PromoteStudentRequest req);
        bool IsStudentExists(String StudentIndexNumber);
        PasswordValidationResponse getStudentPasswordData(String StudentIndexNumber);
        String GetRefreshTokenOwner(String refreshToken);

        void SetRefreshToken(String StudentIndexNumber,String refreshToken);
    }
}
