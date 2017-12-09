namespace Snapbook.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
