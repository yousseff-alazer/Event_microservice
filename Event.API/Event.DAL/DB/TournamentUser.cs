using System;

#nullable disable

namespace Event.API.Event.DAL.DB
{
    public class TournamentUser
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string UserId { get; set; }
        public long? TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}