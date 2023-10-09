using Mapster;
using ToDo.Application.DTOs.User;
using ToDo.Domain.Models;

namespace ToDo.Application.MappingConfig
{
    public class ApplicationUserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, ApplicationUserDto>()
                .Map(dest => dest.Image,
                src => string.IsNullOrEmpty(src.Image) ? null
                : $"http://romeya-001-site4.ctempurl.com/Images/UserPicture/{src.Image}");
        }
    }
}
