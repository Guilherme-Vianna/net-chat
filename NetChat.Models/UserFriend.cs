using NetChat.Models.Base;

namespace NetChat.Models
{
    public class UserFriend : AuditEntity
    {
        public UserFriend()
        {

        }

        public UserFriend(Guid userId, Guid friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }

        public User User { get; set; }
        public Guid UserId { get; set; }
        public User Friend { get; set; }
        public Guid FriendId { get; set; }
    }
}
