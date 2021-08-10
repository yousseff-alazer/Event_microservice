using Event.CommonDefinitions.Records;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Responses
{
    public class TypeTranslateResponse : BaseResponse
    {
        [JsonProperty("Data")] public List<TypeTranslateRecord> TypeTranslateRecords { get; set; }
    }
}