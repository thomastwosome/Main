using System;

namespace Induction.Core
{
    public class BaseEntity
    {
        public virtual int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
