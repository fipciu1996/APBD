namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using WebApplication1.Models;

    [ApiController]
    [Route("api/students")]
    public class StudentsController : Controller
    {
        [HttpGet]
        public List<Student> GetStudents()
        {
            return SelectAllStudents();
        }

        [HttpGet]
        public string GetStudents([FromQuery] string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski sortowanie={orderBy}";
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudnetById(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }

            return NotFound("Nie znaleziono studenta");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {

            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }

        public List<Student> SelectAllStudents()
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s15157;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Students";
                con.Open();
                var dr = com.ExecuteReader();
                List<Student> students = new List<Student>();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr.GetString(dr.GetOrdinal("IndexNumber"));
                    st.FirstName = dr.GetString(dr.GetOrdinal("FirstName"));
                    st.LastName = dr.GetString(dr.GetOrdinal("LastName"));
                    st.BirthDate = dr.GetDateTime(dr.GetOrdinal("BirthDate"));
                    st.Studies = dr.GetString(dr.GetOrdinal("Studies"));
                    students.Add(st);
                }
                return students;
            }
        }
    }
}
