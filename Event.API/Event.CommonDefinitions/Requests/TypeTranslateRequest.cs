using Event.CommonDefinitions.Records;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Requests
{
    public class TypeTranslateRequest : BaseRequest
    {
        public TypeTranslateRecord TypeTranslateRecord { get; set; }

        public List<TypeTranslateRecord> TypeTranslateRecords { get; set; }
    }
}