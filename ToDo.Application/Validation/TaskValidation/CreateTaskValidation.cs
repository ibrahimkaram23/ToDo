using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Abstractions;

namespace ToDo.Application.Validation.TaskValidation
{
    internal class CreateTaskValidation : IValidation<CreateTaskDto>
    {
        private readonly IGenericRepository<Domain.Models.Workspace> _workspaceRepo;

        public CreateTaskValidation(IGenericRepository<Domain.Models.Workspace> workspaceRepo)
        {
            _workspaceRepo = workspaceRepo;
        }
        public StringBuilder Error { get; set; } = new StringBuilder();

        public async Task Validate(CreateTaskDto DTO)
        {
            if (await _workspaceRepo.GetById(DTO.WorkspaceId) == null)
                Error.AppendLine("worspace is not found");
        }
    }
}
