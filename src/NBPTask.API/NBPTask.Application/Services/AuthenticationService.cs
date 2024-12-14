using Microsoft.Extensions.Configuration;
using NBPTask.Application.DTO;
using NBPTask.Application.Errors;
using NBPTask.Domain.Repositories;
using NBPTask.Shared.Results;

namespace NBPTask.Application.Services;

public class AuthenticationService(IUserRepository userRepository, 
    IJwtManager jwtManager,
    IConfiguration configuration) : IAuthenticationService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtManager _jwtManager = jwtManager;
    private readonly IConfiguration _configuration = configuration;

    public Result<AuthenticationDto> SignIn(string username, string password)
    {
        var user = _userRepository.Get(username);
        if (user is null)
        {
            return Result<AuthenticationDto>.Fail(new IncorrectPasswordOrUserNameError());
        }
        
        //NOTE: in real-world scenario we would introduce password hashing
        if (user!.Password != password)
        {
            return Result<AuthenticationDto>.Fail(new IncorrectPasswordOrUserNameError());
        }

        var accessToken = _jwtManager.GenerateToken(user.Username, 
            _configuration["Jwt:Secret"]!, 
            _configuration["Jwt:Issuer"]!, 
            _configuration["Jwt:Audience"]!);
        
        return new AuthenticationDto(accessToken);
    }
}