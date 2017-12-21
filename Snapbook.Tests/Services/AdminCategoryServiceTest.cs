namespace Snapbook.Tests.Services
{
    using FluentAssertions;
    using Snapbook.Data;
    using Snapbook.Data.Models;
    using Snapbook.Services.Admin.Implementations;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AdminCategoryServiceTest
    {
        private readonly SnapbookDbContext db;

        public AdminCategoryServiceTest()
        {
            this.db = Tests.GetDatabase();
            this.PopulateDb();
        }

        [Fact]
        public async Task DeleteShouldReturnFalseWithInvalidId()
        {
            // Arrange
            //Tests.Initialize();
            var adminCategoryService = new AdminCategoryService(this.db);

            // Act
            var result = await adminCategoryService.Delete(10);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task DeleteShouldReturnCorrectResults()
        {
            // Arrange
            //Tests.Initialize();
            var adminCategoryService = new AdminCategoryService(this.db);

            // Act
            var categoriesCountBefore = this.db.Categories.Count();
            var result = await adminCategoryService.Delete(1);
            var categoriesCountAfter = this.db.Categories.Count();

            // Assert
            result.Should().Be(true);
            categoriesCountBefore.Should().Be(1);
            categoriesCountAfter.Should().Be(0);
        }

        private async void PopulateDb()
        {
            var category1 = new Category { Id = 1, Name = "Test" };
            
            this.db.Categories.Add(category1);

            await this.db.SaveChangesAsync();
        }
    }
}
