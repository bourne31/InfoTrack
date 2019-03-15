using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfoTrack.DevProject.Web.Models
{
    public class GoogleSearchViewModel
    {
        [Required(ErrorMessage = "Domain url is required")]
        [Display(Name = "Domain Url")]
        public string DomainUrl { get; set; }

        [Required(ErrorMessage = "Search text is required")]
        [Display(Name = "Search Text")]
        public string SearchText { get; set; }
    }
}
