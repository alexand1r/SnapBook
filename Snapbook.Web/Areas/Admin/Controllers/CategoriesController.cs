namespace Snapbook.Web.Areas.Admin.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Categories;
    using Services.Admin;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
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
        [ValidateRecaptcha]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.categories.Create(
                model.Name);

            this.TempData.AddSuccessMessage($"Category {model.Name} has been successfully created.");
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await this.categories.Find(id);

            if (category == null)
            {
                return this.RedirectToAction("NotFoundPage", "Home", new {Area=""});
            }

            return this.View(new EditCategoryViewModel
            {
                Name = category.Name
            });
        }

        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> Edit(int id, EditCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var success = await this.categories.Edit(
                id,
                model.Name);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"Category {model.Name} details have been successfully changed.");
            return this.RedirectToAction(nameof(this.All));
        }
        
        public async Task<string> Delete(int id)
        {
            var success = await this.categories.Delete(id);

            if (!success)
            {
                return $"Category Not Found.";
            }

            return $"The category has been successfully deleted.";
        }
    }
}
