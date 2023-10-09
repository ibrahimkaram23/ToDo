using System.ComponentModel.DataAnnotations;
using ToDo.Domain.Models.Enums;

namespace ToDo.Application.DTOs
{
    public class UpdateTaskDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public TaskPriority Priority { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        [Required]
        public long WorkspaceId { get; set; }
    }
}
