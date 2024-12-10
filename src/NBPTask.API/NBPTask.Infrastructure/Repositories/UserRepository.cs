using NBPTask.Domain.Entieties;
using NBPTask.Domain.Repositories;

namespace NBPTask.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    //NOTE: mocking user data data source
    private readonly IReadOnlyCollection<User> _users = [new() {Username = "admin", Password = "password"}];  
    
    public User? Get(string username)
        => _users.SingleOrDefault(u => u.Username == username);
}