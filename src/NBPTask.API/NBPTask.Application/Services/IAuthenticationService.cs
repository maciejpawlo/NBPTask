using NBPTask.Application.DTO;
using NBPTask.Shared.Results;

namespace NBPTask.Application.Services;

public interface IAuthenticationService
{
    Result<AuthenticationDto> SignIn(string username, string password);
}