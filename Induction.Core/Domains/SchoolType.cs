using Induction.Core.Domains.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Induction.Core.Domains
{
    public class SchoolType : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        [Required]
        [StringLength(50), Index(IsUnique = true)]
        public string Type { get; set; }

        [Required]
        [StringLength(20)]
        public string Nickname { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Fee { get; set; }

        public RoleType ResponsibleParty { get; set; } //Admin = 1, Teacher = 2, Provider = 3, Hybrid = 4
    }

    //1, EDCOE Charter SELPA, $1000, SchoolSite
    //2, EDCOE Consortium, $0, n/a
    //3, Private School, $3000, Teacher
}
