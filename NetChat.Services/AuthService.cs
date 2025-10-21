using NetChat.Services.Interfaces;
using NetChat.Services.Models.Dto;
using NetChat.Services.Models.ViewModels;
using NetChat.Services.Security;

namespace NetChat.Services
{
    public class AuthService(IUserService userService, IJwtService jwtService) : IAuthService
    {
        public async Task<LoginResponseViewModel> Login(LoginDto dto)
        {
            var userPassword = await userService.GetUserPassword(dto.email);
            var userId = await userService.GetUserId(dto.email);
            if (userPassword == null) throw new Exception("User not found");
            var isPasswordValid = PasswordHasher.VerifyPassword(dto.password, userPassword);
            if (!isPasswordValid) throw new Exception("Invalid password");
            var createTokenDto = new CreateJwtTokenDto(dto.email, userId);
            var token = jwtService.GenerateJwtToken(createTokenDto);
            var response = new LoginResponseViewModel(token);
            return response;
        }
    }
}
