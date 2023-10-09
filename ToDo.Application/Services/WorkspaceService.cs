using MapsterMapper;
using Microsoft.Extensions.Logging;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Application.Specification.WorkspaceSpecification;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Models;

namespace ToDo.Application.Services
{
    internal class WorkspaceService : IWorkspaceService
    {
        private readonly IGenericRepository<Workspace> _workspaceRepo;
        private readonly IValidation<CreateWorkspaceDto> _createWorkspaceValidation;
        private readonly IValidation<UpdateWorkspeceDto> _updateWorkspaceValidation;
        private readonly ILogger<Workspace> _logger;
        private readonly IMapper _mapper;

        public WorkspaceService(IGenericRepository<Workspace> workspaceRepo, 
            IValidation<CreateWorkspaceDto> createWorkspaceValidation, IValidation<UpdateWorkspeceDto> updateWorkspaceValidation,
            ILogger<Workspace> logger, IMapper mapper)
        {
            _workspaceRepo = workspaceRepo;
            _createWorkspaceValidation = createWorkspaceValidation;
            _updateWorkspaceValidation = updateWorkspaceValidation;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseModel> Add(CreateWorkspaceDto workspaceDto)
        {
            try
            {
                await _createWorkspaceValidation.Validate(workspaceDto);

                if (_createWorkspaceValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _createWorkspaceValidation.Error.ToString(),
                    };

                var workspace = _mapper.Map<Workspace>(workspaceDto);

                await _workspaceRepo.Add(workspace);

                await _workspaceRepo.Save();

                return new ResponseModel(true);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public  ResponseModel Get(WorkspaceRequestModel requestModel)
        {
            try
            {
                var result = _workspaceRepo.GetWithSpec(
                    new WorkspacePaginationSpecification(requestModel));

                var workspacesDto = _mapper.Map<IReadOnlyList<WorkspeceDto>>(result.data);

                return new ResponseModel(true)
                {
                    Data = workspacesDto,
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
                var workspace = await _workspaceRepo.GetById(id);

                if (workspace == null)
                    return new ResponseModel(false)
                    {
                        Error = "workspace not found",
                    };

                var workspaceDto = _mapper.Map<WorkspeceDto>(workspace);

                return new ResponseModel(true)
                {
                    Data = workspaceDto,
                };
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> Update(UpdateWorkspeceDto workspeceDto)
        {
            try
            {
                await _updateWorkspaceValidation.Validate(workspeceDto);

                if (_updateWorkspaceValidation.Error.Length > 0)
                    return new ResponseModel(false)
                    {
                        Error = _updateWorkspaceValidation.Error.ToString(),
                    };

                var workspace = await _workspaceRepo.GetById(workspeceDto.Id);

                workspace!.Update(workspeceDto.Title, workspeceDto.Description);

                _workspaceRepo.Update(workspace);

                await _workspaceRepo.Save();

                return new ResponseModel(true)
                {
                    Data = workspace,
                };
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }

        public async Task<ResponseModel> Delete(long id)
        {
            try
            {
                var workspace = await _workspaceRepo.GetById(id);

                if (workspace == null)
                    return new ResponseModel(false)
                    {
                        Error = "workspace not found",
                    };

                _workspaceRepo.Delete(workspace);

                await _workspaceRepo.Save();

                return new ResponseModel(true);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return new ResponseModel(false);
        }
    }
}
