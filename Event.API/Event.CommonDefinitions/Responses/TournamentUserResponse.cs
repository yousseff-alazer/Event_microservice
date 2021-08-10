using Event.CommonDefinitions.Records;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Responses
{
    public class TournamentUserResponse : BaseResponse
    {
        [JsonProperty("Data")] public List<TournamentUserRecord> TournamentUserRecords { get; set; }
    }
}