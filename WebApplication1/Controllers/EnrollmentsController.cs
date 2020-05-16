namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Data;
    using System.Data.SqlClient;
    using WebApplication1.DTOs.Requests;
    using WebApplication1.DTOs.Responses;
    using WebApplication1.Models;
    using WebApplication1.Services;

    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _services;

        public EnrollmentsController(IStudentDbService service)
        {
            _services = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            _services.EnrollStudent(request);
            var response = new EnrollStudentResponse();
            return Ok(response);
        }

        [HttpPost("promotions")]
        public IActionResult Promotions(EnrollPromotionsRequest request)
        {
            if (!ModelState.IsValid)
            {
                var d = ModelState;
                return BadRequest("!!!");
            }

            var enroll = new Enrollment();

            using (var con = new SqlConnection("Data Source=localhost,1433;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=Mssql1234!"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "SELECT IdStudy FROM Studies WHERE name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    var IdStudy = (int)com.ExecuteScalar();

                    com.CommandText = "select IdEnrollment FROM Enrollment WHERE IdStudy=@IdStudy AND Semester=@Semester";
                    com.Parameters.AddWithValue("Semester", (int)request.Semester);
                    com.Parameters.AddWithValue("IdStudy", IdStudy);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {

                        return BadRequest("Wpis w tabeli Enrollment nie istnieje");

                    }

                    dr.Close();
                    SqlCommand cmd = new SqlCommand("dbo.PromoteStudents", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Studies", request.Studies);
                    cmd.Parameters.AddWithValue("@Semester", request.Semester);
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();

                    dr.Close();
                    com.CommandText = "SELECT IdEnrollment FROM Enrollment WHERE IdStudy=@IdStudy AND Semester=@Semester";
                    enroll.IdEnrollment = (int)com.ExecuteScalar();
                    enroll.IdStudy = IdStudy;
                    enroll.Semester = (int)request.Semester + 1;

                    com.CommandText = "SELECT StartDate FROM Enrollment WHERE IdStudy=@IdStudy AND Semester=@Semester";
                    dr.Close();
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        enroll.StartDate = dr.GetDateTime(dr.GetOrdinal("StartDate"));
                    }



                    dr.Close();
                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }

            }
            return Ok(enroll);
        }
    }
}
