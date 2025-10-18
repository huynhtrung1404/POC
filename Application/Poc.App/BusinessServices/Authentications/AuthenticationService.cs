using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Poc.App.Commons;
using Poc.App.Options;
using Poc.App.Services;
using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.Authentications;

public class AuthenticationService(IRepository<Token> tokenRepository,
    IUnitOfWork unitOfWork,
    IRepository<User> userRepository,
    IOptionsSnapshot<JwtConfig> jwtConfig,
    IUserService userService) : IAuthenticationService
{
    private readonly IRepository<Token> _tokenRepository = tokenRepository;
    private readonly IRepository<User> _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly JwtConfig _jwtConfigValue = jwtConfig.Value;
    private readonly IUserService _userService = userService;

    public async Task<AuthenticationDto> LoginAsync(LoginDto login)
    {
        if (string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
        {
            throw new Exception("Invalid information to login");
        }
        var data = await _userRepository.GetItemAsync(new UserSpecification(login.UserName, Helper.ComputeSHA512Hash(login.Password), true))
            ?? throw new Exception("User is not exsting");
        var expiredTime = login.IsRemember ? 100 : 60;
        var token = new Token()
        {
            AccessToken = GenerateToken(login.UserName, data.Email!, Guid.NewGuid().ToString(), expiredTime),
            RefreshToken = GenerateRefreshToken(),
            UserId = data.Id,
            ExpireTime = DateTime.UtcNow.AddDays(1)
        };
        await _tokenRepository.AddAsync(token);
        await _unitOfWork.SaveChangeAsync();
        return new()
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
            ExpireTime = DateTime.UtcNow.AddMinutes(expiredTime)
        };

    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task RegisterAsync(RegisterDto register)
    {
        var data = new User
        {
            Email = register.Email,
            UserName = register.UserName,
            Password = Helper.ComputeSHA512Hash(register.Password!),
            Address = register.Address,
            Age = register.Age
        };
        await _userRepository.AddAsync(data);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<AuthenticationDto> RefreshTokenAsync(RefreshTokenDto model)
    {
        var token = await _tokenRepository.GetItemAsync(new TokenSpecification(model.RefreshToken!))
            ?? throw new Exception("No data found");
        var newAccessToken = GenerateToken(_userService.UserName, _userService.Email, _userService.SessionId, 10);
        token.AccessToken = newAccessToken;
        await _unitOfWork.SaveChangeAsync();
        return new()
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
            ExpireTime = token.ExpireTime
        };
    }

    private string GenerateToken(string username, string email, string sessionId, int expiredTime)
    {

        var claims = new List<Claim>
        {
            new(ConstantClaim.UserName, username),
            new(ConstantClaim.Email, email),
            new(ConstantClaim.Session, sessionId),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigValue.Key!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtConfigValue.Issuer,
            audience: _jwtConfigValue.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiredTime),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}