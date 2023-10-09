using ToDo.Application.DTOs;

namespace ToDo.Application.Abstractions
{
    public interface IWorkspaceService
    {
        public Task<ResponseModel> Add(CreateWorkspaceDto workspaceDto);

        public ResponseModel Get(WorkspaceRequestModel requestModel);

        public Task<ResponseModel> GetById(long id);

        public Task<ResponseModel> Update(UpdateWorkspeceDto workspeceDto);

        public Task<ResponseModel> Delete(long id);
    }
}
