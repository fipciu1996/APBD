namespace WebApplication1.DTOs.Responses
{
    using System;

    public class EnrollStudentResponse
    {
        public string IndexNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Studies { get; set; }
    }
}
