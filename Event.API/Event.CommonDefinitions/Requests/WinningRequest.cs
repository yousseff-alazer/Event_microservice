using Event.CommonDefinitions.Records;

namespace Event.CommonDefinitions.Requests
{
    public class WinningRequest : BaseRequest
    {
        public WinningRecord WinningRecord { get; set; }
    }
}