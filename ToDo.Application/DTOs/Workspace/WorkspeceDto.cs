namespace ToDo.Application.DTOs
{
    public class WorkspeceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public double Progress => Tasks.Count == 0 ? 0 : Tasks.Where(x => x.IsCompleted == true).Count() / (double)Tasks.Count;
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
