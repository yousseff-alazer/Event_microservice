using System;

namespace Event.CommonDefinitions.Records
{
    public class WinningRecord
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
        public string ConstantType { get; set; }

    }
}