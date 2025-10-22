using NetChat.Services.Models.ViewModels;

namespace NetChat.Services.Interfaces
{
    public interface IChatService
    {
        Task<ChatSearchViewModel> GetNewChat(List<string> tags, Guid user_id);
    }
}
