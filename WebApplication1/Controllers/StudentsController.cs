using System;
using System.Collections.Generic;
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
            using (var con = new SqlConnection("Data Source=localhost,1433;Initial Catalog=master;User ID=sa;Password=Mssql1234!"))
            using (var com = new SqlCommand())


        [HttpGet]
        public String GetStudents([FromQuery] String orderBy)
        {

            return $"Kowalski, Malewski, Andrzejewski sortowanie = {orderBy}";
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpGet("student/{id:int}")]
        public IActionResult GetStudentById([FromRoute] int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            } else
            {
                return NotFound("Nie ma takiego studenta");
            }
            
        }
    }
}