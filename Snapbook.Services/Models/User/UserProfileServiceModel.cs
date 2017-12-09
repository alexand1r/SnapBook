namespace Snapbook.Services.Models.User
{
    using Common.Mapping;
    using Data.Models;
    using Models.Album;
    using System;
    using System.Collections.Generic;

    public class UserProfileServiceModel : IMapFrom<User>
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public IEnumerable<UserProfileAlbumServiceModel> Albums { get; set; }
    }
}
