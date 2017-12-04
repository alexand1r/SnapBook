namespace Snapbook.Data.Models
{
    public class UsersLikedImages
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int PhotoId { get; set; }

        public Photo Photo { get; set; }
    }
}
