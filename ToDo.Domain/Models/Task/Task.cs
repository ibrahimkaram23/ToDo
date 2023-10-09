using ToDo.Domain.Models.Enums;

namespace ToDo.Domain.Models
{
    public class Task : BaseModelWithId
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public virtual Workspace Workspace { get; set; }
        public long WorkspaceId { get; set; }

        public void Update(string title, string? description, DateTime dueDate, TaskPriority taskPriority, bool IsCompleted, long workspaceId) 
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = taskPriority;
            WorkspaceId = workspaceId;
            UpdatedAt = DateTime.Now;
        }
    }
}
