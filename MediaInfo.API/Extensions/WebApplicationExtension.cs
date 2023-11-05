namespace MediaInfo.API.Extensions
{
    public static class WebApplicationExtension
    {
        public static WebApplication InitializeDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<MediaInfoDbContext>())
                {
                    try
                    {
                        dbContext.Database.Migrate();
                        //DataSeeder.SeedData(dbContext);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return webApp;
        }
    }
}
