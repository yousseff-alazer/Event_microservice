using Event.API.Event.DAL.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Event.CommonDefinitions.Records
{
    public class TournamentRecord
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? NumberOfParticipants { get; set; }
        public string Name { get; set; }
        public string LeaderBoardId { get; set; }
        public long? TypeId { get; set; }
        public string ObjectId { get; set; }
        public string UserHostId { get; set; }
        public string ImageUrl { get; set; }
        public bool? Public { get; set; }
        public string PriceId { get; set; }
        public string ActionTypeId { get; set; }
        public string ObjectTypeId { get; set; }

        public IFormFile FormImage { get; set; }
        public List<TournamentTranslate> Translates { get; internal set; }
        public string TypeName { get; internal set; }
    }
}