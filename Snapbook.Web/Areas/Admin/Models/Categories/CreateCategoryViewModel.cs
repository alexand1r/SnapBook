namespace Snapbook.Web.Areas.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
