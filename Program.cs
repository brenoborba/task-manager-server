using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using TaskManagerServer.Data;
using TaskManagerServer.Repositories;
using TaskManagerServer.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var taskManagerFrontEnd = "_taskManagerFrontEnd";

new EnvLoader()
    .AddEnvFile("config.env")
    .Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    var reader = new EnvReader();
    var dbHostPath = reader["FRONT_END_HOST_PATH"];
    options.AddPolicy(name: taskManagerFrontEnd,
        policy =>
        {
            policy.WithOrigins($"{dbHostPath}")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );

});

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<TaskManagerDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
    );

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

app.UseCors(taskManagerFrontEnd);

app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
