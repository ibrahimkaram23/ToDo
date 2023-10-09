using System.ComponentModel.DataAnnotations;

namespace ToDo.Application.DTOs
{
    public class RevokeTokenDto
    {
        [Required]
        public string Token { get; set; }
    }
}
