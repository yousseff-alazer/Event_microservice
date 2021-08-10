using Event.CommonDefinitions.Records;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Responses
{
    public class TournamentResponse : BaseResponse
    {
        [JsonProperty("Data")] public List<TournamentRecord> TournamentRecords { get; set; }
    }
}