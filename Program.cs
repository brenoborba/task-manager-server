using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using TaskManagerServer.Data;
using TaskManagerServer.Repositories;
using TaskManagerServer.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var taskManagerFrontEnd = "_taskManagerFrontEnd";

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

app.UseCors(taskManagerFrontEnd);

app.MapGet("/", () => "Hello World!");

app.Run();
