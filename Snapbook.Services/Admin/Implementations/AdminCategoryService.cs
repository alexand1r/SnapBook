namespace Snapbook.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Admin.Models.Category;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
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

        public async Task<bool> Edit(int id, string name)
        {
            var category = await this.db
                .Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return false;
            }

            category.Name = name;

            this.db.SaveChanges();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await this.db
                .Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return false;
            }

            this.db.Categories.Remove(category);
            this.db.SaveChanges();

            return true;
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
