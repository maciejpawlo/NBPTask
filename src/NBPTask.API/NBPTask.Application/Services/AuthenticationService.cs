using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NBPTask.Application.DTO;
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
        //NOTE: in real-world scenario error codes could be exported to separate class with const properties
        if (user is null)
        {
            return Result<AuthenticationDto>.Fail(new Error("username_or_password_incorrect"));
        }
        
        //NOTE: in real-world scenario we would introduce password hashing
        if (user!.Password != password)
        {
            return Result<AuthenticationDto>.Fail(new Error("username_or_password_incorrect"));
        }

        var accessToken = _jwtManager.GenerateToken(user.Username, 
            _configuration["Jwt:Secret"]!, 
            _configuration["Jwt:Issuer"]!, 
            _configuration["Jwt:Audience"]!);
        
        return new AuthenticationDto(accessToken);
    }
}