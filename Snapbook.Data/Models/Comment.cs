namespace Snapbook.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PhotoId { get; set; }

        public Photo Photo { get; set; }
    }
}
