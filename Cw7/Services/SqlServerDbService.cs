using Cw7.DTOs.Requests;
using Cw7.DTOs.Responses;
using Cw7.Models;
using Cw7.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public class SqlServerDbService : IStudentsDbService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18776;Integrated Security=True";
        IPasswordService passwordService;

        public SqlServerDbService(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }

        public bool IsStudentExists(String StudentIndexNumber)
        {
            try
            {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {              
                    com.Connection = con;
                    com.CommandText = "Select 1 from Student where IndexNumber=@index";
                    com.Parameters.AddWithValue("@index", StudentIndexNumber);
                    con.Open();
                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        return true;
                    }
                    reader.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest request)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
            {
               
                    com.Connection = con;
                    com.CommandText = "select * from Enrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy where Name=@StudyName and Semester=@Semester";
                    com.Parameters.AddWithValue("@StudyName", request.Studies);
                    com.Parameters.AddWithValue("@Semester", request.Semester);
                    con.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    if (!reader.Read())
                    {
                        return null;
                    }
                    reader.Close();
                    com.Parameters.Clear();
                    com.CommandText = "PromoteStudents";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Study", request.Studies);
                    com.Parameters.AddWithValue("@Semester", request.Semester);
                    reader = com.ExecuteReader();
                    if (reader.Read()) {
                        PromoteStudentResponse response = new PromoteStudentResponse();
                        response.IdEnrollment = int.Parse(reader["IdEnrollment"].ToString());
                        response.Semester= int.Parse(reader["Semester"].ToString());
                        response.Study = reader["Name"].ToString();
                        response.StartDate = DateTime.Parse(reader["StartDate"].ToString());
                        reader.Close();
                        return response;
                    }
                    else
                    {
                        return null;
                    }              
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {

                    com.Connection = con;
                    com.CommandText = "Select * from Student where IndexNumber=@index";
                    com.Parameters.AddWithValue("@index", request.IndexNumber);
                    con.Open();
                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.Read())
                        return null;

                    reader.Close();
                   
                    com.CommandText = "select IdStudy from Studies where Name=@StudyName";
                    com.Parameters.AddWithValue("@StudyName", request.Studies);
                    reader = com.ExecuteReader();
                    if (!reader.Read()) {
                        return null;
                    }
                    int StudyId = int.Parse(reader["IdStudy"].ToString());

                    reader.Close();
                    com.CommandText = "Select IdEnrollment from Enrollment where StartDate=(select Max(StartDate) from Enrollment where IdStudy=@id and Semester=1) and IdStudy=@id and Semester=1";
                    com.Parameters.AddWithValue("@id", StudyId);
                    bool dataPresent = false;
                    int IdEnrollment=0;
                    reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        dataPresent = true;
                        IdEnrollment = int.Parse(reader["IdEnrollment"].ToString());
                    }
                    reader.Close();
                    SqlTransaction transaction= con.BeginTransaction();
                        try
                        {                    
                            com.Transaction = transaction;
                        
                            EnrollStudentResponse response = new EnrollStudentResponse();
                            if (dataPresent)  
                                {
                                  
                                    com.CommandText = "Insert into Student Values(@IndexNumber,@FirstName,@LastName,@BirthDate,@IdEnrollment,@StudentPassword,@Salt,NULL)";
                                    com.Parameters.AddWithValue("@IndexNumber", request.IndexNumber);
                                    com.Parameters.AddWithValue("@FirstName", request.FirstName);
                                    com.Parameters.AddWithValue("@LastName", request.LastName);
                                    com.Parameters.AddWithValue("@BirthDate", request.Birthdate);
                                    com.Parameters.AddWithValue("@IdEnrollment", IdEnrollment);
                                    String salt = passwordService.CreateSalt();
                                    com.Parameters.AddWithValue("@StudentPassword", passwordService.HashPassword(request.Password, salt));
                                    com.Parameters.AddWithValue("@Salt",salt);
                                    com.ExecuteNonQuery();

                                    com.Parameters.Clear();
                                    com.CommandText = "select IdEnrollment,Semester,StartDate,Name from Enrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy where IdEnrollment=@IdEnrollment";
                                    com.Parameters.AddWithValue("@IdEnrollment", IdEnrollment);
                                    reader = com.ExecuteReader();
                                if (reader.Read())
                                {
                                    response.IdEnrollment = int.Parse(reader["IdEnrollment"].ToString());
                                    response.Semester = int.Parse(reader["Semester"].ToString());
                                    response.Study = reader["Name"].ToString();
                                    response.StartDate = DateTime.Parse(reader["StartDate"].ToString());
                                    reader.Close();
                                    transaction.Commit();
                                    transaction.Dispose();
                                    return response;
                                }
                            return null;
                        }
                            else
                            {
                                reader.Close();
                                com.CommandText = "Insert into Enrollment Values((Select ISNULL(Max(IdEnrollment),0)+1 from Enrollment),1,@IdStudy,(SELECT CONVERT(date, getdate())),@StudentPassword,@Salt,NULL)";
                                com.Parameters.AddWithValue("@IdStudy", StudyId);
                                String salt = passwordService.CreateSalt();
                                com.Parameters.AddWithValue("@StudentPassword", passwordService.HashPassword(request.Password, salt));
                                com.Parameters.AddWithValue("@Salt", salt);
                            if (com.ExecuteNonQuery() == 1)
                                {
                                    com.CommandText = "Insert into Student Values(@IndexNumber,@FirstName,@LastName,@BirthDate,(Select Max(IdEnrollment) from Enrollment))";
                                    com.Parameters.AddWithValue("@IndexNumber", request.IndexNumber);
                                    com.Parameters.AddWithValue("@FirstName", request.FirstName);
                                    com.Parameters.AddWithValue("@LastName", request.LastName);
                                    com.Parameters.AddWithValue("@BirthDate", request.Birthdate);
                                    com.ExecuteNonQuery();

                                    com.CommandText = "select IdEnrollment,Semester,StartDate,Name from Enrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy where IdEnrollment=(Select MAX(IdEnrollment) from Enrollment)";
                                    reader = com.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        response.IdEnrollment = int.Parse(reader["IdEnrollment"].ToString());
                                        response.Semester = int.Parse(reader["Semester"].ToString());
                                        response.Study = reader["Name"].ToString();
                                        response.StartDate = DateTime.Parse(reader["StartDate"].ToString());
                                        reader.Close();
                                        transaction.Commit();
                                        transaction.Dispose();
                                        return response;
                                    }
                                return null;
                            }
                                else {
                                return null;
                            }
                            }
                        }
                        catch (Exception ex2) {
                           
                            transaction.Rollback();
                            transaction.Dispose();
                            return null;
                        }
                
                    }
                }
                catch (Exception ex)
                {
                return null;
            }
            }

        public PasswordValidationResponse getStudentPasswordData(String StudentIndexNumber)
        {
            PasswordValidationResponse response = new PasswordValidationResponse();
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "Select StudentPassword,Salt from Student where IndexNumber=@index";
                    com.Parameters.AddWithValue("@index", StudentIndexNumber);
                    con.Open();
                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.Read())
                    {
                        response.Password = reader["StudentPassword"].ToString().Trim();
                        response.Salt = reader["Salt"].ToString().Trim();
                        reader.Close();
                        return response;
                    }
                    reader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public String GetRefreshTokenOwner(string refreshToken)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "Select IndexNumber from Student where RefreshToken=@Token";
                    com.Parameters.AddWithValue("@Token", refreshToken);
                    con.Open();
                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.Read())
                    {
                        string res = reader["IndexNumber"].ToString();
                        reader.Close();
                        return res;
                    }
                    reader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SetRefreshToken(string StudentIndexNumber,String refreshToken)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "Update Student Set RefreshToken=@Token where IndexNumber=@IndexNumber";
                    com.Parameters.AddWithValue("@Token", refreshToken);
                    com.Parameters.AddWithValue("@IndexNumber", StudentIndexNumber);
                    con.Open();
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {}
        }
    }
    }
