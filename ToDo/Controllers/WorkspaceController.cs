using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;

        public WorkspaceController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel>> Add(CreateWorkspaceDto workspaceDto)
        {
            workspaceDto.UserId = User.FindFirstValue(ClaimTypes.Sid);

            return Ok(await _workspaceService.Add(workspaceDto));
        }
            

        [HttpGet("[action]")]
        public ActionResult<ResponseModel> Get([FromQuery] WorkspaceRequestModel requestModel)
        {
            requestModel.UserId = User.FindFirstValue(ClaimTypes.Sid);

            return Ok(_workspaceService.Get(requestModel));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ResponseModel>> GetById([FromQuery] long id)
            => Ok(await _workspaceService.GetById(id));

        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseModel>> Update(UpdateWorkspeceDto workspaceDto)
        {
            workspaceDto.UserId = User.FindFirstValue(ClaimTypes.Sid);

            return Ok(await _workspaceService.Update(workspaceDto));
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<ResponseModel>> Delete(long id)
            => Ok(await _workspaceService.Delete(id));
    }
}
