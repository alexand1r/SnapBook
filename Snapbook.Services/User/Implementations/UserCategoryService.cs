namespace Snapbook.Services.User.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Category;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserCategoryService : IUserCategoryService
    {
        private readonly SnapbookDbContext db;

        public UserCategoryService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryServiceModel>> AllAsync()
            => await this.db
                .Categories
                .OrderBy(c => c.Name)
                .ProjectTo<CategoryServiceModel>()
                .ToListAsync();
    }
}
