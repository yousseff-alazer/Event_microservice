using Event.CommonDefinitions.Records;
using System;
using System.Linq;
using TournamentUser = Event.API.Event.DAL.DB.TournamentUser;

namespace Event.BL.Services.Managers
{
    public class TournamentUserServiceManager
    {
        private const string TournamentUserPath = "{0}/ContentFiles/TournamentUser/{1}";

        public static TournamentUser AddOrEditTournamentUser(string baseUrl /*, long createdBy*/, TournamentUserRecord record, TournamentUser oldTournamentUser = null)
        {
            if (oldTournamentUser == null) //new tournamentUser
            {
                oldTournamentUser = new TournamentUser();
                oldTournamentUser.CreationDate = DateTime.Now;
                oldTournamentUser.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldTournamentUser.ModificationDate = DateTime.Now;
                oldTournamentUser.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.UserId)) oldTournamentUser.UserId = record.UserId;
            if (record.TournamentId!=null&&record.TournamentId>0) oldTournamentUser.TournamentId = record.TournamentId;
            return oldTournamentUser;
        }

        public static IQueryable<TournamentUserRecord> ApplyFilter(IQueryable<TournamentUserRecord> query, TournamentUserRecord tournamentUserRecord)
        {
            if (tournamentUserRecord.Id > 0)
                query = query.Where(c => c.Id == tournamentUserRecord.Id);
            //if (tournamentUserRecord.Valid != null && tournamentUserRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);

            if (!string.IsNullOrWhiteSpace(tournamentUserRecord.UserId))
                query = query.Where(c => c.UserId != null && c.UserId.Trim().Contains(tournamentUserRecord.UserId.Trim()));

            return query;
        }
    }
}