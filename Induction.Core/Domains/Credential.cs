using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Induction.Core.Domains
{
    public class Credential : BaseEntity
    {
        public Credential()
        {
            People = new HashSet<Person>();
        }

        [Required]
        [StringLength(20), Index(IsUnique = true)]
        [Display(Name = "Nickname")]
        public string CredentialShort { get; set; }

        [Required]
        [StringLength(50), Index(IsUnique = true)]
        [Display(Name = "Credential")]
        public string CredentialLong { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public override string ToString()
        {
            return CredentialShort;
        }
    }
}
