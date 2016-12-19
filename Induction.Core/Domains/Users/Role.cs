using Microsoft.AspNet.Identity.EntityFramework;

namespace Induction.Core.Domains.Users
{
    public class Role : IdentityRole<int, UserRole>
    {
        public Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}
