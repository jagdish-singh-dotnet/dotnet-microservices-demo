using AuthService.Application.Abstractions.Persistence;
using AuthService.Application.Abstractions.Security;
using AuthService.Application.Contracts.Requests;
using AuthService.Application.Contracts.Responses;
using AuthService.Domain.Entities;

namespace AuthService.Application.Features.Auth.Commands
{
    public class RegisterUserCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommand(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResult> ExecuteAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResult
                {
                    IsSuccess = false,
                    Error = "User already exists"
                };
            }

            var hash = _passwordHasher.Hash(request.Password);
            var user = new User(request.Email, hash);

            await _userRepository.AddAsync(user);

            return new AuthResult
            {
                IsSuccess = true
            };
        }
    }
}
