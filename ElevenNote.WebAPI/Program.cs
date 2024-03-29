using ElevenNote.Data;
using Microsoft.EntityFrameworkCore;
using ElevenNote.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add connection string and DbContext setup
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Add User Service/Interface for Dependency Injection here
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adds AuthenticationMiddleware to the IApplicationBuilder
// This enables authentication capabilities
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();