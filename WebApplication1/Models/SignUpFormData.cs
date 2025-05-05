using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SignUpFormData
    {

        [Required]
        
        public string Email { get; set; } = null!;



        [Required]
        public string Password { get; set; } = null!;

    }
}
