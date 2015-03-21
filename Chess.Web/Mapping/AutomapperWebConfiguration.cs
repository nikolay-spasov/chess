using AutoMapper;

using Chess.Core.Models;
using DB = Chess.Infrastructure.Database;

namespace Chess.Web.Mapping
{
    public static class AutomapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<WebMappingProfile>();
            });
        }
    }

    internal class WebMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<DB.User, User>();
            Mapper.CreateMap<User, DB.User>()
                .ForMember(dest => dest.PasswordSalt, opts => opts.UseValue(null));
        }
    }
}