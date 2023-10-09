using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using ToDo.Domain.Models.Enums;

namespace ToDo.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Image { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? BirthOfDate { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPExpirationDate { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        [ForeignKey("UserId")]
        public virtual ICollection<Workspace>? Workspaces { get; set; }

        public void Update(string? email, string? phonenumber, string? username, string? fullName, 
            DateTime? birthOfDate, Gender? gender, string? image)
        {
            Email = !string.IsNullOrEmpty(email) ? email : Email;
            PhoneNumber = !string.IsNullOrEmpty(phonenumber) ? phonenumber : PhoneNumber;
            UserName = !string.IsNullOrEmpty(username) ? username : UserName;
            FullName = !string.IsNullOrEmpty(fullName) ? fullName : FullName;
            BirthOfDate = birthOfDate.HasValue ? birthOfDate : BirthOfDate;
            Gender = gender.HasValue ? gender : Gender;
            Image = !string.IsNullOrEmpty(image) ? image : Image;
        }
    }
}
