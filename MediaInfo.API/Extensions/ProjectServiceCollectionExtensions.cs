namespace MediaInfo.API.Extensions
{
    internal static class ProjectServiceCollectionExtensions
    {
        internal static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddAutoMapper(typeof(AutoMapperProfile).Assembly)
                .AddDbContext<MediaInfoDbContext>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddHandlerService()
            ;

        private static IServiceCollection AddHandlerService(this IServiceCollection services) =>
            services
                .AddScoped<IImageInfoHandler, ImageInfoHandler>()
            ;
    }
}
