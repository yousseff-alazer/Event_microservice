using Event.API.Event.DAL.DB;

namespace Event.CommonDefinitions.Requests
{
    public class BaseRequest
    {
        public int DefaultPageSize = 30;
        public eventdbContext _context;

        public bool IsDesc { get; set; }

        public string OrderByColumn { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        //public long? CreatedBy { get; set; }

        public string BaseUrl { get; set; }

        public string LanguageId { get; set; }
    }
}