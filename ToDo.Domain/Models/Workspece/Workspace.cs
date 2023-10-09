namespace ToDo.Domain.Models
{
    public class Workspace : BaseModelWithId
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Task>? Tasks { get; set; }
        public void Update(string title, string? description)
        {
            Title = title; 
            Description = description;
            UpdatedAt = DateTime.Now;
        }
    }
}
