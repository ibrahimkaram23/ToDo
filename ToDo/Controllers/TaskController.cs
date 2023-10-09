using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Application.DTOs.Task;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel>> Add(CreateTaskDto taskDto)
            => Ok(await _taskService.Add(taskDto));

        [HttpGet("[action]")]
        public ActionResult<ResponseModel> Get([FromQuery] TaskRequestModel requestModel)
        {
            requestModel.UserId = User.FindFirstValue(ClaimTypes.Sid);

            return Ok(_taskService.Get(requestModel));
        }
            

        [HttpGet("[action]")]
        public async Task<ActionResult<ResponseModel>> GetById([FromQuery] long id)
            => Ok(await _taskService.GetById(id));

        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseModel>> Update(UpdateTaskDto taskDto)
            => Ok(await _taskService.Update(taskDto));

        [HttpDelete("[action]")]
        public async Task<ActionResult<ResponseModel>> Delete(long id)
            => Ok(await _taskService.Delete(id));
    }
}
