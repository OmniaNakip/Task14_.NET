using System.ComponentModel.DataAnnotations;

namespace Movie_Management_System.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}