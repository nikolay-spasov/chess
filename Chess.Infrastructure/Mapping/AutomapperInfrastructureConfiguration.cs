namespace Chess.Infrastructure.Mapping
{
    using AutoMapper;

    public static class AutomapperInfrastructureConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile(new TempProfile());
                    cfg.AddProfile(new TempProfile());
                });
        }
    }
}
