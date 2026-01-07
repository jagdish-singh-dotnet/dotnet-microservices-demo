using AuthService.Application.Abstractions.Persistence;
using AuthService.Application.Abstractions.Security;
using AuthService.Application.Abstractions.Tokens;
using AuthService.Application.Contracts.Requests;
using AuthService.Application.Contracts.Responses;

namespace AuthService.Application.Features.Auth.Queries
{
    public class LoginUserQuery
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginUserQuery(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> ExecuteAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return new AuthResult
                {
                    IsSuccess = false,
                    Error = "Invalid credentials"
                };
            }

            return new AuthResult
            {
                IsSuccess = true,
                Token = _tokenService.GenerateToken(user.Id, user.Email)
            };
        }
    }
}
