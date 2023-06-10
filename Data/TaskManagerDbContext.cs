using System.Data.Common;
using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using TaskManagerServer.Data.Map;
using TaskManagerServer.Models;

namespace TaskManagerServer.Data;

public class TaskManagerDbContext : DbContext
{
    
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
        : base(options)
    {
    }

    protected void OnConfiguring(DbContextOptionsBuilder options)
    {
        var reader = new EnvReader();
        var dbHost = reader["DB_HOST"];
        var dbName = reader["DB_NAME"];
        var dbUsername = reader["DB_USERNAME"];
        var dbPassword = reader["DB_PASSWORD"];
        
        options.UseSqlServer(
            $"Server={dbHost}; Database={dbName}; User={dbUsername}; Password={dbPassword}");
    }
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<TaskModel> Tasks { get; set; } = null!;

    protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MapUser());
        modelBuilder.ApplyConfiguration(new MapTask());
        base.OnModelCreating(modelBuilder);
    }

}