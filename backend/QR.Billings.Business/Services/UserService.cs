using QR.Billings.Business.BusinessObjects;
using QR.Billings.Business.Entities;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Repositories;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.Interfaces.Services.Base;
using QR.Billings.Business.IO.Login;

namespace QR.Billings.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<(User, string)> AuthenticateAsync(LoginInput input)
        {
            var user = await _userRepository.GetAsync(input.Username, input.Password);
            if(user == null)
            {
                throw new BusinessException("Nome de usuário ou senha está incorreto.");
            }

            var token = _tokenService.GenerateToken(user);

            if (string.IsNullOrEmpty(token))
            {
                throw new BusinessException("Não foi possível realizar o login.");
            }

            user.Password = string.Empty;

            return (user, token);
        }
    }
}
