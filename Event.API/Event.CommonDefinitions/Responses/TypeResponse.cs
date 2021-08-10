using Event.CommonDefinitions.Records;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Responses
{
    public class TypeResponse : BaseResponse
    {
        [JsonProperty("Data")] public List<TypeRecord> TypeRecords { get; set; }
    }
}