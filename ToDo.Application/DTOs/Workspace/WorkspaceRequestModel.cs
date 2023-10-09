using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDo.Application.DTOs
{
    public class WorkspaceRequestModel : RequestModel
    {
        [BindNever]
        public string? UserId { get; set; }
    }
}
