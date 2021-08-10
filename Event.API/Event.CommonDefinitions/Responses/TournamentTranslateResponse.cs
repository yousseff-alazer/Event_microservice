using Event.CommonDefinitions.Records;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Responses
{
    public class TournamentTranslateResponse : BaseResponse
    {
        [JsonProperty("Data")] public List<TournamentTranslateRecord> TournamentTranslateRecords { get; set; }
    }
}