using ToDo.Application.DTOs.Task;
using ToDo.Domain.Specifications;

namespace ToDo.Application.Specification.TaskSpecification
{
    internal class TaskPaginationSpecification : BaseSpecifications<Domain.Models.Task>
    {
        public TaskPaginationSpecification(TaskRequestModel requestModel)
        {
            AddCriteria(x => x.Workspace.UserId == requestModel.UserId);

            if (requestModel.WorkspaceId != null)
                AddCriteria(x => x.WorkspaceId == requestModel.WorkspaceId);

            if (requestModel.TaskPriority != null)
                AddCriteria(x => x.Priority == requestModel.TaskPriority);

            if (!string.IsNullOrEmpty(requestModel.Search))
                AddCriteria(x => x.Title == requestModel.Search);

            ApplyPaging(requestModel.PageSize, requestModel.PageIndex);
        }
    }
}
