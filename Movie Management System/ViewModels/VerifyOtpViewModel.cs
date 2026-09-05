using System.ComponentModel.DataAnnotations;

namespace Movie_Management_System.ViewModels
{
    public class VerifyOtpViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string OTP { get; set; }
    }
}