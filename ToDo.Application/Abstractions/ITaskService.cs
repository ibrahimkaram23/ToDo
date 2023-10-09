using ToDo.Application.DTOs;
using ToDo.Application.DTOs.Task;

namespace ToDo.Application.Abstractions
{
    public interface ITaskService
    {
        public Task<ResponseModel> Add(CreateTaskDto taskDto);

        public ResponseModel Get(TaskRequestModel requestModel);

        public Task<ResponseModel> GetById(long id);

        public Task<ResponseModel> Update(UpdateTaskDto taskDto);

        public Task<ResponseModel> Delete(long id);
    }
}
