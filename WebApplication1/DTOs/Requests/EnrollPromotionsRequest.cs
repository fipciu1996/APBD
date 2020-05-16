namespace WebApplication1.DTOs.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class EnrollPromotionsRequest
    {
        [Required(ErrorMessage = "Musisz podać studia")]
        public string Studies { get; set; }

        [Required(ErrorMessage = "Musisz podać semestr")]
        public int Semester { get; set; }
    }
}
