namespace Snapbook.Web.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Is this an advertisement account?")]
        public bool IsAdvertiser { get; set; }
    }
}
