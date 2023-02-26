using Event.API.Event.DAL.DB;
using Event.CommonDefinitions.Records;
using System;
using System.Linq;

namespace Event.BL.Services.Managers
{
    public class TournamentTranslateServiceManager
    {
        private const string TournamentTranslatePath = "{0}/ContentFiles/TournamentTranslate/{1}";

        public static TournamentTranslate AddOrEditTournamentTranslate(string baseUrl /*, long createdBy*/,
            TournamentTranslateRecord record, TournamentTranslate oldTournamentTranslate = null)
        {
            if (oldTournamentTranslate == null) //new tournamentTranslate
            {
                oldTournamentTranslate = new TournamentTranslate();
                oldTournamentTranslate.CreationDate = DateTime.Now;
                oldTournamentTranslate.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldTournamentTranslate.ModificationDate = DateTime.Now;
                oldTournamentTranslate.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Name)) oldTournamentTranslate.Name = record.Name;
            if (!string.IsNullOrWhiteSpace(record.Description)) oldTournamentTranslate.Description = record.Description;
            if (!string.IsNullOrWhiteSpace(record.LanguageId)) oldTournamentTranslate.LanguageId = record.LanguageId;
            if (record.TournamentId > 0) oldTournamentTranslate.TournamentId = record.TournamentId;
            return oldTournamentTranslate;
        }

        public static IQueryable<TournamentTranslateRecord> ApplyFilter(IQueryable<TournamentTranslateRecord> query,
            TournamentTranslateRecord tournamentTranslateRecord)
        {
            if (tournamentTranslateRecord.Id > 0)
                query = query.Where(c => c.Id == tournamentTranslateRecord.Id);
            if (tournamentTranslateRecord.TournamentId > 0)
                query = query.Where(c => c.TournamentId == tournamentTranslateRecord.TournamentId);
            //if (tournamentTranslateRecord.Valid != null && tournamentTranslateRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);

            //if (!string.IsNullOrWhiteSpace(tournamentTranslateRecord.ObjectTypeId))
            //    query = query.Where(c => c.ObjectTypeId != null && c.ObjectTypeId.Trim().Contains(tournamentTranslateRecord.ObjectTypeId.Trim()));

            return query;
        }
    }
}