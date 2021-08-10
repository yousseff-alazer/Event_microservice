using System;

#nullable disable

namespace Event.API.Event.DAL.DB
{
    public class TournamentTranslate
    {
        public long Id { get; set; }
        public long TournamentId { get; set; }
        public string LanguageId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}