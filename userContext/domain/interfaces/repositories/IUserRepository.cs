using userContext.domain.entities;

namespace domain.interfaces.repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
