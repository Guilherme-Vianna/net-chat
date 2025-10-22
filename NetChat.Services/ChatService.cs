using NetChat.Repository.Interfaces;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.ViewModels;

namespace NetChat.Services
{
    public class ChatService(IUserRepository userRepository, ITagRepository tagRepository) : IChatService
    {
        public async Task<ChatSearchViewModel> GetNewChat(List<string> tags, Guid user_id)
        {
            var tags_ids = new List<Guid>();

            foreach (var tag in tags)
            {
                var tagId = await tagRepository.GetTagIdByName(tag);

                if (tagId != null)
                {
                    tags_ids.Add((Guid)tagId);
                }
            }

            var search = await userRepository.GetUserThatHaveTagList(tags_ids, 1, user_id);
            if (search == null)
            {
                throw new Exception("No user found with the given tags.");
            }
            var response = new ChatSearchViewModel(search.Name, search.Tags.Select(x => x.Tag.Name).ToList(), search.CreatedAt);
            return response;
        }
    }
}
