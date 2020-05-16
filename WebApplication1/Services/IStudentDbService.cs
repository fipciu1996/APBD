namespace WebApplication1.Services
{
    using WebApplication1.DTOs.Requests;
    using WebApplication1.Models;

    public interface IStudentDbService
    {
        void EnrollStudent(EnrollStudentRequest request);

        void PromoteStudents(int semester, string studies);

        Student GetStudent(string IndexNumber);
    }
}
