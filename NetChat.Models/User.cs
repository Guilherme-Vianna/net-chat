using NetChat.Models.Base;

namespace NetChat.Models
{
    public class User : AuditEntity
    {
        public User()
        {

        }

        public User(string email, string passwordHash, string name, List<Tag> tags)
        {
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            Tags = tags;
        }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
