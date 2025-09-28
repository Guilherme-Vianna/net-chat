using NetChat.Models;
using NetChat.Repository.Interfaces;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.Dto;

namespace NetChat.Services;

public class ChatService(IUserRepository userRepository, ITagRepository tagRepository) : IChatService
{
    public async Task<ChatResponseDto> GetChat(List<Guid> tags_ids, Guid userId)
    {
        if (tags_ids.Count == 0) throw new Exception("Tags count are 0");
        var usersIds = await userRepository.GetUsersIdsByTags(tags_ids, userId);
        if (usersIds.Count == 0) throw new Exception("No users found");
        var user = await userRepository.GetUserByIdAsyncWithTags(usersIds.First());
        if (user == null) throw new Exception("User not found");
        var searchTags = new List<Tag>();
        foreach (var tagId in tags_ids)
        {
            var tag = await tagRepository.GetTagById(tagId);
            searchTags.Add(tag);
        }
       
        var response = new ChatResponseDto(user.Id, user.Name, searchTags, user.Tags.Select(x =>x.Tag).ToList());
        return response;
    }
}