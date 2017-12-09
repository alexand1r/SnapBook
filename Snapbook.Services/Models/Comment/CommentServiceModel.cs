namespace Snapbook.Services.Models.Comment
{
    using Common.Mapping;
    using Data.Models;
    using System;

    public class CommentServiceModel : IMapFrom<Comment>
    {
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public User Author { get; set; }
    }
}
