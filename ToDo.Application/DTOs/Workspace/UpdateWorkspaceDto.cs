using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDo.Application.DTOs
{
    public class UpdateWorkspeceDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
