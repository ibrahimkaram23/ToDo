﻿using ToDo.Domain.Models.Enums;

namespace ToDo.Application.DTOs
{
    public class TaskDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public long WorkspaceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
