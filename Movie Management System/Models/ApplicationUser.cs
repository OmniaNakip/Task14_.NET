using Microsoft.AspNetCore.Identity;

namespace Movie_Management_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? PasswordResetOtp { get; set; }

        public DateTime? PasswordResetOtpExpiry { get; set; }
    }
}