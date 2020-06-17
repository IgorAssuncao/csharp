using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("personFriends")]
    public class PersonFriends
    {
        [Key]
        public int Id { get; set; }
        [Column("user_id")]
        public int personId { get; set; }
        [Column("friend_user_id")]
        public int friendId { get; set; }

        public PersonFriends() { }

        public PersonFriends(int person_id, int friend_id)
        {
            personId = person_id;
            friendId = friend_id;
        }
    }
}
