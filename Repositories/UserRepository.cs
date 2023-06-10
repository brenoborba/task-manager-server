using Microsoft.EntityFrameworkCore;
using TaskManagerServer.Data;
using TaskManagerServer.Models;
using TaskManagerServer.Repositories.Interfaces;

namespace TaskManagerServer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskManagerDbContext _dbContext;
    
    public UserRepository(TaskManagerDbContext taskManagerDbContext)
    {
        _dbContext = taskManagerDbContext;
    }
    public async Task<List<UserModel>> SearchAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<UserModel> SearchById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<UserModel> Add(UserModel user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<UserModel> Update(UserModel user, int id)
    {
        UserModel userById = await SearchById(id);

        if (userById == null)
        {
            throw new Exception($"UserID: {id} not found on database");
        }

        userById.Name = user.Name;
        userById.Email = user.Email;

        _dbContext.Users.Update(userById);
        await _dbContext.SaveChangesAsync();
  
        return userById;
    }

    public async Task<bool> Delete(int id)
    {
        UserModel userById = await SearchById(id);
        if (userById == null)
        {
            throw new Exception($"UserID: {id} not found on database");
        }

        _dbContext.Users.Remove(userById);
        await _dbContext.SaveChangesAsync();
        return true;

    }
    
}