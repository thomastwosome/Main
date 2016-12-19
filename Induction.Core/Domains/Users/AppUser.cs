using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Induction.Core.Domains.Users
{
    public class AppUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        #region Constructor(s)

        public AppUser()
        {
            //Applications = new HashSet<Application>();
        }

        #endregion

        #region Scaler Properties

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        #endregion

        #region Navigational Properties

        //public virtual ICollection<Application> Applications { get; set; }

        #endregion

        #region Methods

        public string LastNameFirst()
        {
            return LastName + ", " + FirstName;
        }

        public string FirstNameFirst()
        {
            return FirstName + " " + LastName;
        }

        #endregion

    }
}
