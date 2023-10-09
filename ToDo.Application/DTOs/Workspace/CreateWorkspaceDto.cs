using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDo.Application.DTOs
{
    public class CreateWorkspaceDto
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
