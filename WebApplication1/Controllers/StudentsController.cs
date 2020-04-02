﻿using System;
using System.Collections.Generic;
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
        public String GetStudents([FromQuery] String orderBy)
        {

            return $"Kowalski, Malewski, Andrzejewski sortowanie = {orderBy}";
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