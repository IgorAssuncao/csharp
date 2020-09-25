using System;

namespace Model
{
    public class Friendship
    {
        public Guid PersonId { get; set; }
        public Friend Friend { get; set; }
        public Guid FriendId { get; set; }
    }
}
