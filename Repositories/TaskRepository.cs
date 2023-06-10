using TaskManagerServer.Models;

namespace TaskManagerServer.Repositories;

public class TaskRepository
{
    private readonly TaskManagerDbContext _dbContext;

    public TaskRepository(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TaskModel>> SearchAllTasks()
    {
        return await _dbContext.Tasks.ToListAsync();
    }

    public async Task<TaskModel> SearchById(int id)
    {
        return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TaskModel> Add(TaskModel task)
    {
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
        return task;
    }

    public async Task<TaskModel> Update(TaskModel task, int id)
    {
        TaskModel taskById = await SearchById(id);

        if (taskById == null)
        {
            throw new Exception($"Task: {id} not found on database");
        }

        taskById.Name = task.Name;
        taskById.Description = task.Description;
        taskById.Status = task.Status;

        _dbContext.Update(taskById);
        await _dbContext.SaveChangesAsync();
        return taskById;
    }

    public async Task<bool> Delete(int id)
    {
        TaskModel taskById = await SearchById(id);
        if (taskById == null)
        {
            throw new Exception($"TaskID: {id} not found on database");
        }

        _dbContext.Tasks.Remove(taskById);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}