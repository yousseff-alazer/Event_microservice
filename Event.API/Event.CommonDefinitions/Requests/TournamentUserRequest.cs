using Event.CommonDefinitions.Records;

namespace Event.CommonDefinitions.Requests
{
    public class TournamentUserRequest : BaseRequest
    {
        public TournamentUserRecord TournamentUserRecord { get; set; }
    }
}