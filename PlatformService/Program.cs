using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService;
using PlatformService.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();
builder.Services.AddAutoMapper(typeof(PlatformsProfile).Assembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if (builder.Environment.IsProduction())
{
    System.Console.WriteLine("--->Using SqlServer DB");
    builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}
else
{
    System.Console.WriteLine("--->Using InMem DB");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMem"));
}
System.Console.WriteLine($"CommandService Endpoint: {builder.Configuration.GetSection("CommandServiceUri").Value}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
PrepDb.PrepPopulation(app, builder.Environment.IsProduction());
app.Run();
