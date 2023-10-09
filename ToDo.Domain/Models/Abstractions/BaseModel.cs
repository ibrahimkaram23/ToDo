namespace ToDo.Domain.Models
{
    public class BaseModel
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
