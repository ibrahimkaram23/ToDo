using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Abstractions;

namespace ToDo.Application.Validation.TaskValidation
{
    internal class CreateWorkspaceValidation : IValidation<CreateWorkspaceDto>
    {
        private readonly IGenericRepository<Domain.Models.Workspace> _workspaceRepo;

        public CreateWorkspaceValidation(IGenericRepository<Domain.Models.Workspace> workspaceRepo)
        {
            _workspaceRepo = workspaceRepo;
        }
        public StringBuilder Error { get; set; } = new StringBuilder();

        public async Task Validate(CreateWorkspaceDto DTO)
        {
            if(DTO.UserId == null)
            {
                Error.AppendLine("unAuthorized access");
                return;
            }

            if (await _workspaceRepo.IsExist(x => x.Title == DTO.Title && x.UserId == DTO.UserId))
                Error.AppendLine("worspace title is used before");
        }
    }
}
