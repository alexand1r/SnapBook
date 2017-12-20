namespace Snapbook.Services.Models.User
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Models.Album;
    using Models.Photo;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserProfileServiceModel : UserListingServiceModel, IHaveCustomMapping
    {
        public string Name { get; set; }

        public string Bio { get; set; }

        public DateTime BirthDate { get; set; }

        public IEnumerable<UserProfileAlbumServiceModel> Albums { get; set; }

        public IEnumerable<SavedPhotoListingServiceModel> SavedPhotos { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<User, UserProfileServiceModel>()
                .ForMember(up => up.SavedPhotos, 
                    cfg => cfg.MapFrom(u => u.SavedPhotos.Select(sp => sp.Photo)));
    }
}
