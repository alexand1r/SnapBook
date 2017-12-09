namespace Snapbook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int PhotoId { get; set; }

        public Photo Photo { get; set; }
    }
}
