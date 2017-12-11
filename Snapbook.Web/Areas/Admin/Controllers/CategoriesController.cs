namespace Snapbook.Web.Areas.Admin.Controllers
{
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Categories;
    using Services.Admin;
    using System.Threading.Tasks;

    public class CategoriesController : BaseController
    {
        private readonly IAdminCategoryService categories;

        public CategoriesController(IAdminCategoryService categories)
        {
            this.categories = categories;
        }

        public async Task<IActionResult> All()
            => this.View(await this.categories.All());

        public IActionResult Create()
            => this.View();

        [HttpPost]
        [ValidateModelState]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            this.categories.Create(
                model.Name);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await this.categories.Find(id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(new EditCategoryViewModel
            {
                Name = category.Name
            });
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Edit(int id, EditCategoryViewModel model)
        {
            this.categories.Edit(
                id,
                model.Name);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
