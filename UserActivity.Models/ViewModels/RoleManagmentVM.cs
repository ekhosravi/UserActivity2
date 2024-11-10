using Microsoft.AspNetCore.Mvc.Rendering;
using UserActivity.Models;

namespace UserActivity.Models.ViewModels {
    public class RoleManagmentVM {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }
}
