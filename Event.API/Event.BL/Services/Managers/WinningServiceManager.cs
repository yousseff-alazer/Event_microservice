using Event.API.Event.DAL.DB;
using Event.CommonDefinitions.Records;
using System;
using System.Linq;

namespace Event.BL.Services.Managers
{
    public class WinningServiceManager
    {
        private const string WinningPath = "{0}/ContentFiles/Winning/{1}";

        public static Winning AddOrEditWinning(string baseUrl /*, long createdBy*/, WinningRecord record,
            Winning oldWinning = null)
        {
            if (oldWinning == null) //new winning
            {
                oldWinning = new Winning();
                oldWinning.CreationDate = DateTime.Now;
                oldWinning.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldWinning.ModificationDate = DateTime.Now;
                oldWinning.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Amount)) oldWinning.Amount = record.Amount;
            if (record.Order!=null&&record.Order>0) oldWinning.Order = record.Order;
            if (!string.IsNullOrWhiteSpace(record.ConstantType)) oldWinning.ConstantType = record.ConstantType;
            if (record.TournamentId != null && record.TournamentId > 0) oldWinning.TournamentId = record.TournamentId;
            return oldWinning;
        }

        public static IQueryable<WinningRecord> ApplyFilter(IQueryable<WinningRecord> query,
            WinningRecord winningRecord)
        {
            if (winningRecord.Id > 0)
                query = query.Where(c => c.Id == winningRecord.Id);

            if (winningRecord.TournamentId > 0)
                query = query.Where(c => c.TournamentId == winningRecord.TournamentId);
            //if (winningRecord.Valid != null && winningRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);

            //if (!string.IsNullOrWhiteSpace(winningRecord.ObjectTypeId))
            //    query = query.Where(c => c.ObjectTypeId != null && c.ObjectTypeId.Trim().Contains(winningRecord.ObjectTypeId.Trim()));

            return query;
        }
    }
}