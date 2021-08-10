using Event.CommonDefinitions.Records;

namespace Event.CommonDefinitions.Requests
{
    public class TournamentTranslateRequest : BaseRequest
    {
        public TournamentTranslateRecord TournamentTranslateRecord { get; set; }
    }
}