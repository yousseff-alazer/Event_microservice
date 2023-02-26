using System.Collections.Generic;
using Event.CommonDefinitions.Records;

namespace Event.CommonDefinitions.Requests
{
    public class TournamentTranslateRequest : BaseRequest
    {
        public TournamentTranslateRecord TournamentTranslateRecord { get; set; }
        public List<TournamentTranslateRecord> TournamentTranslateRecords { get; set; }
    }
}