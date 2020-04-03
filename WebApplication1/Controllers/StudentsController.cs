using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public List<Student> GetStudents()
        {
            return SelectAllStudents();
        }

        [HttpPut("student/{id:int}")]
        public IActionResult UpdateStudent(Student Student)
        {
            return Ok("Aktualizacja zakończona");
        }

        [HttpDelete("student/{id:int}")]
        public IActionResult RemoveStudent()
        {
            return Ok("Usuwanie zakończone");
        }


        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            student.AddStudent();
            return Ok(student);
        }

        [HttpGet("student/{id:int}")]
        public IActionResult GetStudentById([FromRoute] int id)
        {
            Student student = SelectStudentById(id);
            if (student.IdStudent != 0)
            {
                return Ok(student);
            } else
            {
                return NotFound("Nie ma takiego studenta");
            }
            
        }

        private Student SelectStudentById(int id)
        {
            using (var con = new SqlConnection("Data Source=localhost,1433;Initial Catalog=master;User ID=sa;Password=Mssql1234!"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Students WHERE IdStudent = @IdStudent";
                com.Parameters.Add("@IdStudent", SqlDbType.Int).Value = id;
                con.Open();
                var dr = com.ExecuteReader();
                var st = new Student();
                while (dr.Read())
                {
                    st.IdStudent = int.Parse(dr["IdStudent"].ToString());
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                }
                return st;
            }
        }

        public List<Student> SelectAllStudents()
        {
            using (var con = new SqlConnection("Data Source=localhost,1433;Initial Catalog=master;User ID=sa;Password=Mssql1234!"))
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
                    st.IdStudent = int.Parse(dr["IdStudent"].ToString());
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    students.Add(st);
                }
                return students;
            }
        }
    }
}