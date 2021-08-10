using Event.CommonDefinitions.Records;

namespace Event.CommonDefinitions.Requests
{
    public class TypeRequest : BaseRequest
    {
        public TypeRecord TypeRecord { get; set; }
    }
}