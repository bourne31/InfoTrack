using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfoTrack.DevProject.Web.Models
{
    public class SearchViewModel
    {
        [Display(Name = "Domain Url")]
        public string DomainUrl { get; set; }

        [Display(Name = "Search Text")]
        public string SearchText { get; set; }

        public string Rankings { get; set; }
    }
}
