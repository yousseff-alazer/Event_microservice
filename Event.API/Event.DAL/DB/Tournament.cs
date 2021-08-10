using System;
using System.Collections.Generic;

#nullable disable

namespace Event.API.Event.DAL.DB
{
    public class Tournament
    {
        public Tournament()
        {
            TournamentTranslates = new HashSet<TournamentTranslate>();
            TournamentUsers = new HashSet<TournamentUser>();
            Winnings = new HashSet<Winning>();
        }

        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? NumberOfParticipants { get; set; }
        public string Name { get; set; }
        public string LeaderBoardId { get; set; }
        public long? TypeId { get; set; }
        public string ObjectId { get; set; }
        public string UserHostId { get; set; }
        public string ImageUrl { get; set; }
        public bool? Public { get; set; }
        public string PriceId { get; set; }
        public string ActionTypeId { get; set; }
        public string ObjectTypeId { get; set; }

        public virtual Type Type { get; set; }
        public virtual ICollection<TournamentTranslate> TournamentTranslates { get; set; }
        public virtual ICollection<TournamentUser> TournamentUsers { get; set; }
        public virtual ICollection<Winning> Winnings { get; set; }
    }
}