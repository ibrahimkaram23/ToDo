using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ToDo.Domain.Models;
using ToDo.Infrastructure.Context;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ToDo.DI
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudiance"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";
                                var message = new { message = "access token has expired" };
                                var json = JsonSerializer.Serialize(message);
                                return context.Response.WriteAsync(json);
                            }
                            return System.Threading.Tasks.Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
