namespace NetChat.Services.Models.ViewModels
{
    public record MessageViewModel(Guid id, string message, Guid sender_id, Guid recipent_id, DateTime created_at);
}
