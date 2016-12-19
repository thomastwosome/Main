using System.ComponentModel.DataAnnotations;

namespace Induction.Core.Domains.Enums
{
    public enum RoleType
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "Participating Teacher")]
        PT = 2,
        [Display(Name = "Support Provider")]
        SP = 3,
        [Display(Name = "Admin/SP")]
        Hybrid = 4
    }
}
