using Cw9.DTOs.Requests;
using Cw9.DTOs.Responses;
using Cw9.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw9.Services
{
    public class SqlServerDbService : IStudentsDbService
    {
        s18776Context dbContext;

        public SqlServerDbService(s18776Context dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool IsStudentExists(String StudentIndexNumber)
        {
            
            return dbContext.Student.Any()?dbContext.Student.Where(student=>student.IndexNumber==StudentIndexNumber).FirstOrDefault()==null:false;
        }
        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request)
        {
            try
            {
                if (!dbContext.Enrollment.Any() || !dbContext.Studies.Any())
                    return null;
                var enrollment = dbContext.Enrollment
                                          .Join(dbContext.Studies,
                                                el1 => el1.IdStudy,
                                                el2 => el2.IdStudy,
                                                (el1, el2) => new { Semester = el1.Semester, el2.Name })
                                          .Where(el => el.Semester == request.Semester && el.Name == request.Studies)
                                          .FirstOrDefault();
                if (enrollment == null)
                    return null;

                SqlParameter param1 = new SqlParameter("@Study", request.Studies);
                SqlParameter param2 = new SqlParameter("@Semester", request.Semester);
                var tmpProc = dbContext.Enrollment.FromSqlRaw("exec PromoteStudents @Study,@Semester", param1, param2)
                                                   .ToList()
                                                   .SingleOrDefault();
                if (tmpProc == null)
                    return null;

                return new PromoteStudentResponse
                {
                    IdEnrollment = tmpProc.IdEnrollment,
                    StartDate = tmpProc.StartDate,
                    Semester = tmpProc.Semester,
                    Name = dbContext.Studies
                                  .Where(study => study.IdStudy == tmpProc.IdStudy)
                                  .Select(study => study.Name).FirstOrDefault()
                };
            }
            catch (Exception ex) {
                return new PromoteStudentResponse { Name=ex.ToString()};
            }
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                if (IsStudentExists(request.IndexNumber) || !dbContext.Studies.Any())
                    return null;
                var Study = dbContext.Studies.Where(study => study.Name == request.Studies).FirstOrDefault();
                if (Study == null)
                    return null;

                int StudyId = Study.IdStudy;
            var enrollment = dbContext.Enrollment.Where(en2 => en2.IdStudy == StudyId && en2.Semester == 1 && en2.StartDate == dbContext.Enrollment.Where(en => en.IdStudy == StudyId && en.Semester == 1).Max(en => en.StartDate)).FirstOrDefault();
                if (enrollment!=null )
                {
                    var tmpStudent = new Student
                    {
                        IndexNumber=request.IndexNumber,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        BirthDate = request.Birthdate,
                        IdEnrollment = enrollment.IdEnrollment
                    };

                    dbContext.Add(tmpStudent);
                    dbContext.SaveChanges();

                    var Result = dbContext.Enrollment
                                .Where(el => el.IdEnrollment == enrollment.IdEnrollment)
                                .Join(dbContext.Studies,
                                      (el1) => el1.IdStudy,
                                      (el2) => el2.IdStudy,
                                      (el1, el2) => new { IdEnrollment = el1.IdEnrollment, Semester = el1.Semester, StartDate = el1.StartDate, Study = el2.Name })
                                .FirstOrDefault();

                    return new EnrollStudentResponse
                    {
                        IdEnrollment = Result.IdEnrollment,
                        Study = Result.Study,
                        StartDate = Result.StartDate,
                        Semester = Result.Semester
                    };

                }
                else
                {
                var NewEnrollment = new Enrollment
                {
                        IdEnrollment = dbContext.Enrollment.Any() ? dbContext.Enrollment.Max(e => e.IdEnrollment) + 1:1,
                        IdStudy = StudyId,
                        Semester = 1,
                        StartDate = DateTime.Now
                    };
                    dbContext.Add(NewEnrollment);
                    dbContext.SaveChanges();

                    var tmpStudent = new Student
                    {
                        IndexNumber = request.IndexNumber,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        BirthDate = request.Birthdate,
                        IdEnrollment = NewEnrollment.IdEnrollment
                    };

                    dbContext.Add(tmpStudent);
                    dbContext.SaveChanges();

                    var Result = dbContext.Enrollment
                               .Where(el => el.IdEnrollment == NewEnrollment.IdEnrollment)
                               .Join(dbContext.Studies,
                                     (el1) => el1.IdStudy,
                                     (el2) => el2.IdStudy,
                                     (el1, el2) => new { IdEnrollment = el1.IdEnrollment, Semester = el1.Semester, StartDate = el1.StartDate, Study = el2.Name })
                               .FirstOrDefault();

                    return new EnrollStudentResponse
                    {
                        IdEnrollment = Result.IdEnrollment,
                        Study = Result.Study,
                        StartDate = Result.StartDate,
                        Semester = Result.Semester
                    };
                }
            }
            catch (Exception ex) {
                return new EnrollStudentResponse { Study = ex.ToString() };
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            return dbContext.Student.ToList();
        }

        public bool DeleteStudent(string StudentIndexNumber)
        {
            try
            {
                var student = dbContext.Student.Where(stud => stud.IndexNumber == StudentIndexNumber).FirstOrDefault();
                if (student == null)
                    return false;

                dbContext.Student.Remove(student);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public bool UpdateStudent(UpdateStudentRequest request)
        {
            try
            {
                var student = dbContext.Student.Where(student => student.IndexNumber == request.IndexNumber).FirstOrDefault();
                if (student == null)
                    return false;

                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.IdEnrollment = request.IdEnrollment;
                student.BirthDate = request.BirthDate;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
