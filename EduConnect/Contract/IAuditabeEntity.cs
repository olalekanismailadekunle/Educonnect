﻿using System;

namespace EduConnect.Contract
{
    public interface IAuditabeEntity
    {
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
