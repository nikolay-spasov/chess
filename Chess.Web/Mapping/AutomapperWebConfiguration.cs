namespace Chess.Web.Mapping
{
    using AutoMapper;

    using Chess.Core.Models;
    using Chess.Infrastructure.Database.Entities;

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
            Mapper.CreateMap<DbUser, User>();
            Mapper.CreateMap<User, DbUser>()
                .ForMember(dest => dest.PasswordSalt, opts => opts.Ignore());
        }
    }
}