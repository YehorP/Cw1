using Cw10.DTOs.Request;
using Cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Services
{
    public interface IDbService
    {
        public IEnumerable<Doctor> GetDoctors();

        public bool DeleteDoctor(int IdDoctor);

        public bool AddDoctor(AddDoctorRequest request);
        public bool UpdateDoctor(UpdateDoctorRequest request);
    }
}
