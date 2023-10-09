using System.Text;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Domain.Abstractions;

namespace ToDo.Application.Validation.TaskValidation
{
    internal class UpdateTaskValidation : IValidation<UpdateTaskDto>
    {
        private readonly IGenericRepository<Domain.Models.Workspace> _workspaceRepo;
        private readonly IGenericRepository<Domain.Models.Task> _taskRepo;

        public UpdateTaskValidation(IGenericRepository<Domain.Models.Workspace> workspaceRepo, IGenericRepository<Domain.Models.Task> taskRepo)
        {
            _workspaceRepo = workspaceRepo;
            _taskRepo = taskRepo;
        }
        public StringBuilder Error { get; set; } = new StringBuilder();

        public async Task Validate(UpdateTaskDto DTO)
        {
            if (await _taskRepo.GetById(DTO.Id) == null)
                Error.AppendLine("task is not found");

            if (await _workspaceRepo.GetById(DTO.WorkspaceId) == null)
                Error.AppendLine("worspace is not found");
        }
    }
}
