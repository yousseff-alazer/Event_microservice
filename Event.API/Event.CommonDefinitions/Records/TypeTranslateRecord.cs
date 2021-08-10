using System;

namespace Event.CommonDefinitions.Records
{
    public class TypeTranslateRecord
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
    }
}