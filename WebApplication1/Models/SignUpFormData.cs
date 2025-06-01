using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SignUpFormData
    {

        [Required]
        [MinLength(5, ErrorMessage = "Email måste vara minst 5 tecken långt.")]
        public string Email { get; set; } = null!;



        [Required]
        [MinLength(8, ErrorMessage = "Lösenordet måste vara minst 8 tecken långt.")]
        public string Password { get; set; } = null!;

    }
}
