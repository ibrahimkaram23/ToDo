using ToDo.Application.DTOs;
using ToDo.Domain.Models;
using ToDo.Domain.Specifications;

namespace ToDo.Application.Specification.WorkspaceSpecification
{
    internal class WorkspacePaginationSpecification : BaseSpecifications<Workspace>
    {
        public WorkspacePaginationSpecification(WorkspaceRequestModel requestModel)
        {
            AddCriteria(x => x.UserId == requestModel.UserId);

            AddInclude(x => x.Tasks!);

            if (!string.IsNullOrEmpty(requestModel.Search))
                AddCriteria(x => x.Title == requestModel.Search);

            ApplyPaging(requestModel.PageSize, requestModel.PageIndex);
        }
    }
}
