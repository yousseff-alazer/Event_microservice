using Event.CommonDefinitions.Records;
using System;
using System.Linq;
using Type = Event.API.Event.DAL.DB.Type;

namespace Event.BL.Services.Managers
{
    public class TypeServiceManager
    {
        private const string TypePath = "{0}/ContentFiles/Type/{1}";

        public static Type AddOrEditType(string baseUrl /*, long createdBy*/, TypeRecord record, Type oldType = null)
        {
            if (oldType == null) //new type
            {
                oldType = new Type();
                oldType.CreationDate = DateTime.Now;
                oldType.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldType.ModificationDate = DateTime.Now;
                oldType.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Name)) oldType.Name = record.Name;

            return oldType;
        }

        public static IQueryable<TypeRecord> ApplyFilter(IQueryable<TypeRecord> query, TypeRecord typeRecord)
        {
            if (typeRecord.Id > 0)
                query = query.Where(c => c.Id == typeRecord.Id);
            //if (typeRecord.Valid != null && typeRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);

            if (!string.IsNullOrWhiteSpace(typeRecord.Name))
                query = query.Where(c => c.Name != null && c.Name.Trim().Contains(typeRecord.Name.Trim()));

            return query;
        }
    }
}