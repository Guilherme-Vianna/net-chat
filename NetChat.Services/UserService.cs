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
    public class UserService(IUserRepository repository, ITagRepository tagRepository, IMessageRepository messageRepository) : IUserService
    {
        public async Task<UserViewModel> GetUser(Guid userId)
        {
            var userSearch = await repository.GetUserByIdWithTagsAsync(userId);
            if (userSearch == null) throw new Exception("User not found");
            var result = new UserViewModel(
                id: userSearch.Id, 
                email: userSearch.Email, 
                name: userSearch.Name, 
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

            var tags = new List<Tag>();

            foreach (var newTag in createUserDto.tags)
            {
                var tag = new Tag(newTag);
                tags.Add(await tagRepository.CreateIfNotExistOrReturnIfExist(tag));
            }

            var newUser = new User(createUserDto.email, passwordHash, createUserDto.name, tags.Select(x => x.Id).ToList());
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

            var tags = new List<Tag>();

            foreach (var newTag in dto.tags)
            {
                var tag = new Tag(newTag);
                tags.Add(await tagRepository.CreateIfNotExistOrReturnIfExist(tag));
            }

            user.UpdateBasicProperties(dto.email, dto.name, tags.Select(x => x.Id).ToList());
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

        public Task<UserViewModel> GetData()
        {
            throw new NotImplementedException();
        }

        public async Task AddFriend(AddUserFriendDto dto)
        {
            await repository.StartTransaction();
            await repository.AddUserFriend(dto.user_id, dto.friend_id);
            await repository.CommitTransaction();
        }

        public async Task<List<FriendViewModel>> GetFriends(Guid userId)
        {
            var userFriends = await repository.GetUserFriendsQueryable(userId);
            var result = new List<FriendViewModel>();

            foreach (var friend in userFriends)
            {
                var lastMessage = await messageRepository.GetLastMessage(userId, friend.FriendId);
                var userFriend = await repository.GetUserByIdAsync(friend.FriendId); 
                if(userFriend == null) throw new Exception("Friend user not found");

                result.Add(new FriendViewModel(
                    lastMessage?.Data ?? null,
                    lastMessage?.CreatedAt ?? null,
                    userFriend.Name,
                    friend.FriendId
                ));
            }

            return result;
        }
    }
}
