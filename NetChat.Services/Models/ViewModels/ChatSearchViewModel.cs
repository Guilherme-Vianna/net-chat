namespace NetChat.Services.Models.ViewModels
{
    public class ChatSearchViewModel
    {
        public ChatSearchViewModel(string username, List<string> tags, DateTime user_createdat)
        {
            this.username = username;
            this.tags = tags;
            this.user_createdat = user_createdat;
        }

        public string username { get; set; }
        public List<string> tags { get; set; }
        public DateTime user_createdat { get; set; }
    }
}
