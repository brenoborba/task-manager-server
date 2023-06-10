using TaskManagerServer.Models;

namespace TaskManagerServer.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserModel>> SearchAllUsers();
    Task<UserModel?> SearchById(int id);
    Task<UserModel> Add(UserModel user);
    Task<UserModel> Update(UserModel user, int id);
    Task<bool> Delete(int id);
}