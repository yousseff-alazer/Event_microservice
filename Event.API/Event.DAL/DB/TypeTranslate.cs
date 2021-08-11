using System;
using System.Collections.Generic;

#nullable disable

namespace Event.API.Event.DAL.DB
{
    public partial class TypeTranslate
    {
        public long Id { get; set; }
        public long TypeId { get; set; }
        public string LanguageId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Type Type { get; set; }
    }
}
