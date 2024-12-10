using NBPTask.Domain.Entieties;

namespace NBPTask.Domain.Repositories;

public interface IUserRepository
{
    User? Get(string username);
}