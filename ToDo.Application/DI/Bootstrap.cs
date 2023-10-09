using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Abstractions;
using ToDo.Application.DTOs;
using ToDo.Application.DTOs.User;
using ToDo.Application.Services;
using ToDo.Application.Validation.OTPValidation;
using ToDo.Application.Validation.PasswordValidation;
using ToDo.Application.Validation.RefreshTokenValidation;
using ToDo.Application.Validation.TaskValidation;
using ToDo.Application.Validations.AuthValidation;

namespace ToDo.Application.DI
{
    public static class Bootstrap
    {
        public static void ApplicationStrapping(this IServiceCollection services)
        {
            #region Validators
            services.AddScoped<IValidation<RefreshTokenDto>, RefreshTokenValidation>();
            services.AddScoped<IValidation<RevokeTokenDto>, RevokeTokenValidation>();


            services.AddScoped<IValidation<ResetPasswordWithOTPDto>, ResetPasswordWithOTPValidation>();
            services.AddScoped<IValidation<SendOTPDto>, SendOTPValidation>();


            services.AddScoped<IValidation<ChangePasswordDto>, ChangePasswordValidation>();


            services.AddScoped<IValidation<LoginDto>, LoginValidation>();
            services.AddScoped<IValidation<RegisterDto>, RegisterValidation>();
            services.AddScoped<IValidation<UpdateUserDto>, UpdateUserValidation>();


            services.AddScoped<IValidation<CreateWorkspaceDto>, CreateWorkspaceValidation>();
            services.AddScoped<IValidation<UpdateWorkspeceDto>, UpdateWorkspaceValidation>();


            services.AddScoped<IValidation<CreateTaskDto>, CreateTaskValidation>();
            services.AddScoped<IValidation<UpdateTaskDto>, UpdateTaskValidation>();
            #endregion

            #region Services
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IPasswordService, PasswordService>();

            services.AddScoped<IOTPService, OTPService>();

            services.AddScoped<IWorkspaceService, WorkspaceService>();

            services.AddScoped<ITaskService, TaskService>();
            #endregion
        }
    }
}
