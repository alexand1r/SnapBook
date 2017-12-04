namespace Snapbook.Data.Models
{
    using System.Collections.Generic;

    public class Ad
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
