using csharp_url_shortener_api.Dtos.AuthDtos;
using csharp_url_shortener_api.Helpers;
using csharp_url_shortener_api.Interfaces.Repositories;
using csharp_url_shortener_api.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace csharp_url_shortener_api.Services;

public class AuthService : IAuthService
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IPasswordHasherService passwordHasher, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto> Login(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByUsername(loginDto.Username);

        if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password, user.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = _jwtTokenService.GenerateToken(user);

        var loginResponseDto = MappingProfile.ToLoginResponseDto(user);

        loginResponseDto.Token = token;
        loginResponseDto.ExpiresAt = DateTime.UtcNow.AddHours(24);

        return loginResponseDto;
    }

    public async Task<LoginResponseDto> Register(RegisterDto registerDto)
    {
        var user = await _userRepository.GetUserByUsername(registerDto.Username);
    
        // if (user != null) throw new ConflictException("Username already exists");
    
        var hashedPassword = _passwordHasher.HashPassword(registerDto.Password);
    
        var newUser = MappingProfile.ToUser(registerDto);
        newUser.Password = hashedPassword;
    
        var createdUser = await _userRepository.CreateUser(newUser);
    
        var token = _jwtTokenService.GenerateToken(createdUser);
    
        var loginResponseDto = MappingProfile.ToLoginResponseDto(createdUser);
    
        loginResponseDto.Token = token;
        loginResponseDto.ExpiresAt = DateTime.UtcNow.AddHours(24);
    
        return loginResponseDto;
    }
}