namespace Snapbook.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ExistsAsync(string name)
            => await this.db.Categories.AnyAsync(c => c.Name == name);
    }
}
