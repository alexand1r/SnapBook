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

        public string AlbumAuthor { get; set; }

        public string AlbumAuthorId { get; set; }

        public string AlbumAuthorUrl { get; set; }

        public string AlbumTitle { get; set; }

        public string AdAuthor { get; set; }

        public string AdUrl { get; set; }

        public string AdAuthorId { get; set; }

        public string AdName { get; set; }

        public int? AlbumId { get; set; }

        public int? AdId { get; set; }

        public IEnumerable<CommentServiceModel> Comments { get; set; }

        public IEnumerable<TagListingServiceModel> Tags { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Photo, PhotoDetailsServiceModel>()
                .ForMember(pd => pd.Likes, cfg => cfg.MapFrom(p => p.Likers.Count))
                .ForMember(pd => pd.AlbumAuthor, cfg => cfg.MapFrom(p => p.Album.User.UserName))
                .ForMember(pd => pd.AlbumAuthorId, cfg => cfg.MapFrom(p => p.Album.User.Id))
                .ForMember(pd => pd.AlbumTitle, cfg => cfg.MapFrom(p => p.Album.Title))
                .ForMember(pd => pd.AlbumAuthorUrl, cfg => cfg.MapFrom(p => p.Album.User.ProfilePicUrl))
                .ForMember(pd => pd.AdAuthor, cfg => cfg.MapFrom(p => p.Ad.User.UserName))
                .ForMember(pd => pd.AdAuthorId, cfg => cfg.MapFrom(p => p.Ad.User.Id))
                .ForMember(pd => pd.AdName, cfg => cfg.MapFrom(p => p.Ad.Name))
                .ForMember(pd => pd.AdUrl, cfg => cfg.MapFrom(p => p.Ad.AdProfilePicUrl));
    }
}
