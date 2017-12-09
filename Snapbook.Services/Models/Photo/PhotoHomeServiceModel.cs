namespace Snapbook.Services.Models.Photo
{ 
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Models.Tag;
    using System;
    using System.Collections.Generic;

    public class PhotoHomeServiceModel : IMapFrom<Photo>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }

        public string Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }

        public int AlbumId { get; set; }
        
        public IEnumerable<TagListingServiceModel> Tags { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Photo, PhotoHomeServiceModel>()
                .ForMember(pd => pd.Likes, cfg => cfg.MapFrom(p => p.Likers.Count))
                .ForMember(pd => pd.Comments, cfg => cfg.MapFrom(p => p.Comments.Count))
                .ForMember(pd => pd.Author, cfg => cfg.MapFrom(p => p.Album.User.UserName))
                .ForMember(pd => pd.AuthorId, cfg => cfg.MapFrom(p => p.Album.User.Id));
    }
}
