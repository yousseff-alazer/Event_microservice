using System;
using System.Collections.Generic;

#nullable disable

namespace Event.API.Event.DAL.DB
{
    public partial class Winning
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string Amount { get; set; }
        public long? Order { get; set; }
        public long? TournamentId { get; set; }
        public string PriceTypeId { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
