using Event.CommonDefinitions.Records;

namespace Event.CommonDefinitions.Requests
{
    public class TournamentRequest : BaseRequest
    {
        public TournamentRecord TournamentRecord { get; set; }
    }
}