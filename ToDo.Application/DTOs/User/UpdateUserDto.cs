using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ToDo.Domain.Models.Enums;

namespace ToDo.Application.DTOs.User
{
    public class UpdateUserDto
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? BirthOfDate { get; set; }
    }
}
