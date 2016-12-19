using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Induction.Core.Domains
{
    public class Person : BaseEntity
    {
        public Person()
        {
            Credentials = new HashSet<Credential>();
            Grades = new HashSet<Grade>();
        }

        public string UserId { get; set; }

        public int SchoolType { get; set; }

        [Required]
        [StringLength(50), Index("IX_UniqueName", 1, IsUnique = true)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50), Index("IX_UniqueName", 2, IsUnique = true)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Title")]
        [StringLength(50), DefaultValue("")]
        public string Title { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Phone Number")]
        [RegularExpression(CommonConstants.PhoneRegex, ErrorMessage = "Please enter a 10-digit number.")]
        public string PhoneNumber { get; set; }

        [StringLength(10)]
        [Display(Name = "Extension")]
        [DefaultValue("")]
        public string Extension { get; set; }

        [Required]
        [StringLength(100), Index(IsUnique = true)]
        [Display(Name = "Email")]
        [RegularExpression(CommonConstants.EmailRegex, ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Display(Name = "Credential(s)")]
        public ICollection<Credential> Credentials { get; set; }

        [Display(Name = "Grade Level(s)")]
        public ICollection<Grade> Grades { get; set; }

        public string FirstNameFirst
        {
            get
            {
                if (!string.IsNullOrEmpty(MiddleName))
                {
                    return FirstName + " " + MiddleName + " " + LastName;
                }
                return FirstName + " " + LastName;
            }
        }

        public string LastNameFirst
        {
            get
            {
                if (!string.IsNullOrEmpty(MiddleName))
                {
                    return LastName + ", " + FirstName + " " + MiddleName;
                }
                return LastName + ", " + FirstName;
            }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
