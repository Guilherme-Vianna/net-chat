using NetChat.Models;
using NetChat.Repository.Interfaces;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.UpdateDto;
using NetChat.Services.Models.ViewModels;
using NetChat.Services.Security;
using System.ComponentModel;
using NetChat.Services.Models.Dto;

namespace NetChat.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<UserViewModel> GetUser(Guid userId)
        {
            var userSearch = await repository.GetUserByIdAsync(userId);
            if (userSearch == null) throw new Exception("User not found");
            var result = new UserViewModel(
                id: userSearch.Id, 
                email: userSearch.Email, 
                name: userSearch.Email, 
                userTags: userSearch.Tags
            );
            return result;
        }

        public async Task<UserViewModel> CreateAsync(CreateUserDto createUserDto)
        {
            await repository.StartTransaction();
            var emailExist = await repository.ExistEmail(createUserDto.email);
            if (emailExist) throw new Exception("Email exist!");
            
            var passwordHash = PasswordHasher.HashPassword(createUserDto.password);
            var newUser = new User(createUserDto.email, passwordHash, createUserDto.name, createUserDto.tags_ids);
            await repository.CreateUser(newUser);
            
            var result = await repository.GetUserByIdAsyncWithTags(newUser.Id);
            if (result == null) throw new Exception("User not exist!");
            var response = new UserViewModel(result.Id, result.Email, result.Name, result.Tags);
            await repository.CommitTransaction();
            return response;
        }

        public async Task<UserViewModel> UpdateAsync(UpdateUserDto dto)
        {
            await repository.StartTransaction();
            var emailExist = await repository.ExistEmail(dto.email, dto.id);
            if (emailExist) throw new Exception("Email exist!");
            
            var user = await repository.GetUserByIdToEditAsync(dto.id);
            if (user == null) throw new Exception("User not exist!");
            
            user.UpdateBasicProperties(dto.email, dto.name, dto.tags_ids);
            await repository.Update(user);
            var result = await repository.GetUserByIdAsyncWithTags(user.Id);
            if (result == null) throw new Exception("User not found!");
            var response = new UserViewModel(result.Id, result.Email, result.Name, result.Tags);
            
            await repository.CommitTransaction();
            return response;
        }
        
        public async Task<UserViewModel> UpdatePasswordAsync(UpdatePasswordDto dto)
        {
            await repository.StartTransaction();
            var user = await repository.GetUserByIdToEditAsync(dto.id);
            if (user == null) throw new Exception("User not exist!");
            var correctPassword = PasswordHasher.VerifyPassword(dto.old_password, user.PasswordHash);
            if(correctPassword == false) throw new Exception("Password is incorrect!");
            var passwordHash = PasswordHasher.HashPassword(dto.new_password);
            user.UpdatePassword(passwordHash);
            await repository.Update(user);
            var result = await repository.GetUserByIdAsyncWithTags(user.Id);
            if (result == null) throw new Exception("User not found!");
            var response = new UserViewModel(result.Id, result.Email, result.Name, result.Tags);
            await repository.CommitTransaction();
            return response;
        }

        public async Task<string> GetUserPassword(string email)
        {
            var user = await repository.GetUserByEmailAsync(email);
            if (user == null) throw new Exception("User not found"); 
            return user.PasswordHash;
        }

        public async Task<Guid> GetUserId(string email)
        {
            var user = await repository.GetUserByEmailAsync(email);
            if (user == null) throw new Exception("User not found");
            return user.Id;
        }

        public async Task<bool> Exist(string email)
        {
            var emailExist = await repository.ExistEmail(email);
            return emailExist;
        }
    }
}
