using NetChat.Models.Base;

namespace NetChat.Models
{
    public class User : AuditEntity
    {
        public User()
        {

        }

        public User(string email, string passwordHash, string name, List<Guid> tags_ids)
        {
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            var tags = new List<UserTag>();
            foreach (var tagId in tags_ids)
            {
                tags.Add(new UserTag(tagId, Id));
            }
            Tags = tags;
        }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
        public List<UserTag> Tags { get; set; }
    }
}
