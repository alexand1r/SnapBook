namespace Snapbook.Services.Models.Photo
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Models.Comment;
    using Models.Tag;
    using System;
    using System.Collections.Generic;

    public class PhotoDetailsServiceModel : IMapFrom<Photo>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }

        public string Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int Likes { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }

        public IEnumerable<CommentServiceModel> Comments { get; set; }

        public IEnumerable<TagListingServiceModel> Tags { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Photo, PhotoDetailsServiceModel>()
                .ForMember(pd => pd.Likes, cfg => cfg.MapFrom(p => p.Likers.Count))
                .ForMember(pd => pd.Author, cfg => cfg.MapFrom(p => p.Album.User.UserName))
                .ForMember(pd => pd.AuthorId, cfg => cfg.MapFrom(p => p.Album.User.Id));
    }
}
