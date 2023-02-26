using Event.CommonDefinitions.Records;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Requests
{
    public class WinningRequest : BaseRequest
    {
        public WinningRecord WinningRecord { get; set; }
        public List<WinningRecord> WinningRecords { get; set; }
    }
}