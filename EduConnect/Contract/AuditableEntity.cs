using System;

namespace EduConnect.Contract
{
    public abstract class AuditableEntity : BaseEntity, IAuditabeEntity, ISoftDelete
    {
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
        public int LastModifiedBy { get; set; } 
        public DateTime? LastModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime? IsDeleteOn { get; set; }
        public DateTime? IsDeleteBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
