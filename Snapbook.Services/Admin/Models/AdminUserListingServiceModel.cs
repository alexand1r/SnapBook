﻿namespace Snapbook.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
