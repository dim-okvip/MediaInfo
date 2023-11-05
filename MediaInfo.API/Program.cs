using MediaInfo.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
const string CORS_POLICY = "CorsPolicy";

builder.Configuration.AddJsonFile("appsettings.json", false, true);

builder.Services.AddCors(o => o.AddPolicy(CORS_POLICY, builder =>
{
    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProjectServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MediaInfo v1");
    options.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();

app.UseCors(CORS_POLICY);

app.UseAuthorization();

app.MapControllers();

app.InitializeDatabase();

app.Run();
