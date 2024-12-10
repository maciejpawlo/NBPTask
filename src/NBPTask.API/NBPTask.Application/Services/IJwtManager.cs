using NBPTask.Domain.Entieties;

namespace NBPTask.Application.Services;

public interface IJwtManager
{
    string GenerateToken(string username, string secret, string issuer, string audience);
}