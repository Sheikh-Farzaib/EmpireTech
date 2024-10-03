using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommonEntity
{
    public class AuditableEntity
    {
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; } = false; 
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
