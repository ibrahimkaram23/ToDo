using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDo.Domain.Models.Enums;

namespace ToDo.Application.DTOs.Task
{
    public class TaskRequestModel : RequestModel
    {
        public long? WorkspaceId { get; set; }
        public TaskPriority? TaskPriority { get; set; }
        [BindNever]
        public string? UserId { get; set; }
    }
}
