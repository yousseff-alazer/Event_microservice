using System;
using System.Collections.Generic;

#nullable disable

namespace Event.API.Event.DAL.DB
{
    public partial class Type
    {
        public Type()
        {
            Tournaments = new HashSet<Tournament>();
            TypeTranslates = new HashSet<TypeTranslate>();
        }

        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
        public virtual ICollection<TypeTranslate> TypeTranslates { get; set; }
    }
}
