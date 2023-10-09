using System.ComponentModel.DataAnnotations;
using ToDo.Domain.Models.Enums;

namespace ToDo.Application.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
        public bool IsCompleted => false;
        [Required]
        public long WorkspaceId { get; set; }
    }
}
