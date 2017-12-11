namespace Snapbook.Services.Models.Photo
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class SavedPhotoListingServiceModel : IMapFrom<Photo>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string AlbumAuthor { get; set; }

        public string AlbumAuthorId { get; set; }

        public string AdAuthor { get; set; }

        public string AdAuthorId { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Photo, SavedPhotoListingServiceModel>()
                .ForMember(spl => spl.AlbumAuthor, cfg => cfg.MapFrom(p => p.Album.User.UserName))
                .ForMember(spl => spl.AlbumAuthorId, cfg => cfg.MapFrom(p => p.Album.UserId))
                .ForMember(spl => spl.AdAuthor, cfg => cfg.MapFrom(p => p.Ad.User.UserName))
                .ForMember(spl => spl.AdAuthorId, cfg => cfg.MapFrom(p => p.Ad.UserId))
                .ForMember(spl => spl.Likes, cfg => cfg.MapFrom(p => p.Likers.Count))
                .ForMember(spl => spl.Comments, cfg => cfg.MapFrom(p => p.Comments.Count));
    }
}
