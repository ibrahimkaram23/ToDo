using ToDo.Domain.Models.Enums;

namespace ToDo.Application.DTOs.User
{
    public class ApplicationUserDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Image { get; set; }
        public string PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? BirthOfDate { get; set; }
    }
}
