namespace Snapbook.Data.Models
{
    public class UsersFollowers
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public User Follower { get; set; }
        public string FollowerId { get; set; }
    }
}
