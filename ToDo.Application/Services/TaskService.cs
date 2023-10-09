using MapsterMapper;
using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Application.DTOs.Task;
using ToDo.Application.Specification.TaskSpecification;
using ToDo.Domain.Abstractions;

namespace ToDo.Application.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IGenericRepository<Domain.Models.Task> _taskRepo;
        private readonly IValidation<CreateTaskDto> _createTaskValidation;
        private readonly IValidation<UpdateTaskDto> _updateTaskValidation;
        private readonly ILogger<Domain.Models.Task> _logger;
        private readonly IMapper _mapper;

        public TaskService(IGenericRepository<Domain.Models.Task> taskRepo,
            IValidation<CreateTaskDto> createTaskValidation, IValidation<UpdateTaskDto> updateTaskValidation,
            ILogger<Domain.Models.Task> logger, IMapper mapper)
        {
            _taskRepo = taskRepo;
            _createTaskValidation = createTaskValidation;
            _updateTaskValidation = updateTaskValidation;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseModel> Add(CreateTaskDto taskDto)
        {
            try
            {
                await _createTaskValidation.Validate(taskDto);

                if (_createTaskValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _createTaskValidation.Error.ToString(),
                    };

                var workspace = _mapper.Map<Domain.Models.Task>(taskDto);

                await _taskRepo.Add(workspace);

                await _taskRepo.Save();

                return new ResponseModel(true);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public ResponseModel Get(TaskRequestModel requestModel)
        {
            try
            {
                var result = _taskRepo.GetWithSpec(
                    new TaskPaginationSpecification(requestModel));

                var taskesDto = _mapper.Map<IReadOnlyList<TaskDto>>(result.data);

                return new ResponseModel(true)
                {
                    Data = result.data,
                    TotalCount = result.count,
                };
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> GetById(long id)
        {
            try
            {
                var task = await _taskRepo.GetById(id);

                if (task == null)
                    return new ResponseModel(false)
                    {
                        Error = "task not found",
                    };

                var taskDto = _mapper.Map<WorkspeceDto>(task);

                return new ResponseModel(true)
                {
                    Data = taskDto,
                };
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> Update(UpdateTaskDto taskDto)
        {
            try{
                await _updateTaskValidation.Validate(taskDto);

                if (_updateTaskValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _updateTaskValidation.Error.ToString(),
                    };

                var task = await _taskRepo.GetById(taskDto.Id);

                task!.Update(taskDto.Title, taskDto.Description, taskDto.DueDate, taskDto.Priority, 
                    taskDto.IsCompleted, taskDto.WorkspaceId);

                _taskRepo.Update(task);

                await _taskRepo.Save();

                return new ResponseModel(true)
                {
                    Data = task,
                };
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> Delete(long id)
        {
            try
            {
                var task = await _taskRepo.GetById(id);

                if (task == null)
                    return new ResponseModel(false)
                    {
                        Error = "task not found",
                    };

                _taskRepo.Delete(task);

                await _taskRepo.Save();

                return new ResponseModel(true);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }
    }
}
