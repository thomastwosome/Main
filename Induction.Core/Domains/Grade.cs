using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Induction.Core.Domains
{
    public class Grade : BaseEntity
    {
        public Grade()
        {
            People = new HashSet<Person>();
        }

        [Required]
        [StringLength(20), Index(IsUnique = true)]
        public string GradeLevel { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public override string ToString()
        {
            return GradeLevel;
        }
    }
}
