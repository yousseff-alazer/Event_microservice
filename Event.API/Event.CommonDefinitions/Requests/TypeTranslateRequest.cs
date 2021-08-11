using System.Collections.Generic;
using Event.CommonDefinitions.Records;

namespace Event.CommonDefinitions.Requests
{
    public class TypeTranslateRequest : BaseRequest
    {
        public TypeTranslateRecord TypeTranslateRecord { get; set; }

        public List<TypeTranslateRecord> TypeTranslateRecords { get; set; }
    }
}