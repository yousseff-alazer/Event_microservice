using Event.API.Event.DAL.DB;
using Event.CommonDefinitions.Records;
using System;
using System.Linq;

namespace Event.BL.Services.Managers
{
    public class TypeTranslateServiceManager
    {
        private const string TypeTranslatePath = "{0}/ContentFiles/TypeTranslate/{1}";

        public static TypeTranslate AddOrEditTypeTranslate(string baseUrl /*, long createdBy*/,
            TypeTranslateRecord record, TypeTranslate oldTypeTranslate = null)
        {
            if (oldTypeTranslate == null) //new typeTranslate
            {
                oldTypeTranslate = new TypeTranslate();
                oldTypeTranslate.CreationDate = DateTime.Now;
                oldTypeTranslate.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldTypeTranslate.ModificationDate = DateTime.Now;
                oldTypeTranslate.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Name)) oldTypeTranslate.Name = record.Name;
            if (!string.IsNullOrWhiteSpace(record.LanguageId)) oldTypeTranslate.LanguageId = record.LanguageId;
            if (record.TypeId > 0) oldTypeTranslate.TypeId = record.TypeId;
            return oldTypeTranslate;
        }

        public static IQueryable<TypeTranslateRecord> ApplyFilter(IQueryable<TypeTranslateRecord> query,
            TypeTranslateRecord typeTranslateRecord)
        {
            if (typeTranslateRecord.Id > 0)
                query = query.Where(c => c.Id == typeTranslateRecord.Id);
            if (typeTranslateRecord.TypeId > 0)
                query = query.Where(c => c.TypeId == typeTranslateRecord.TypeId);
            //if (typeTranslateRecord.Valid != null && typeTranslateRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);

            if (!string.IsNullOrWhiteSpace(typeTranslateRecord.LanguageId))
                query = query.Where(c => c.LanguageId != null && c.LanguageId.Trim().Contains(typeTranslateRecord.LanguageId.Trim()));

            return query;
        }
    }
}