using Event.CommonDefinitions.Records;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Responses
{
    public class WinningResponse : BaseResponse
    {
        [JsonProperty("Data")] public List<WinningRecord> WinningRecords { get; set; }
    }
}