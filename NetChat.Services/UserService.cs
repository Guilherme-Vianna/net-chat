using NetChat.Models;
using NetChat.Repository.Interfaces;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.CreateDto;
using NetChat.Services.Models.ViewModels;
using NetChat.Services.Security;

namespace NetChat.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<UserViewModel> CreateAsync(CreateUserDto createUserDto)
        {
            var emailExist = await repository.ExistEmail(createUserDto.email);
            if (emailExist)  throw new Exception("Email exist!"); 
            var passwordHash = PasswordHasher.HashPassword(createUserDto.password);
            var newUser = new User(createUserDto.email, passwordHash, createUserDto.name, createUserDto.tags_ids);
            var result = await repository.CreateUser(newUser);
            return new UserViewModel(result.Id, result.Email, result.Name, result.Tags);
        }
    }
}
