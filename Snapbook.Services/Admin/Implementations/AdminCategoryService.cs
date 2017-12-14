namespace Snapbook.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Snapbook.Services.Admin.Models.Category;

    public class AdminCategoryService : IAdminCategoryService
    {
        private readonly SnapbookDbContext db;

        public AdminCategoryService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public void Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            this.db.Categories.Add(category);
            this.db.SaveChanges();
        }

        public void Edit(int id, string name)
        {
            var category = this.db
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return;
            }

            category.Name = name;

            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = this.db
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return;
            }

            this.db.Categories.Remove(category);
            this.db.SaveChanges();
        }

        public async Task<bool> ExistsAsync(string name)
            => await this.db.Categories.AnyAsync(c => c.Name == name);

        public async Task<CategoryListingServiceModel> Find(int id)
            => await this.db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryListingServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<CategoryListingServiceModel>> All()
            => await this.db
                .Categories
                .ProjectTo<CategoryListingServiceModel>()
                .ToListAsync();
    }
}
