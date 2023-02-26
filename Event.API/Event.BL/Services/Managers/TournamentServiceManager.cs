using Event.API.Event.DAL.DB;
using Event.CommonDefinitions.Records;
using System;
using System.IO;
using System.Linq;

namespace Event.BL.Services.Managers
{
    public class TournamentServiceManager
    {
        private const string TournamentPath = "{0}/ContentFiles/Tournament/{1}";

        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static Tournament AddOrEditTournament(string baseUrl /*, long createdBy*/, TournamentRecord record,
            Tournament oldTournament = null)
        {
            if (oldTournament == null) //new tournament
            {
                oldTournament = new Tournament
                {
                    CreationDate = DateTime.Now,
                    CreatedBy = record.CreatedBy,
                    Code = GetCodeResult()
                };
            }
            else
            {
                oldTournament.ModificationDate = DateTime.Now;
                oldTournament.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Name)) oldTournament.Name = record.Name;
            if (!string.IsNullOrWhiteSpace(record.Description)) oldTournament.Description = record.Description;

            if (!string.IsNullOrWhiteSpace(record.ObjectId)) oldTournament.ObjectId = record.ObjectId;
            if (!string.IsNullOrWhiteSpace(record.ObjectTypeId)) oldTournament.ObjectTypeId = record.ObjectTypeId;
            if (!string.IsNullOrWhiteSpace(record.ActionTypeId)) oldTournament.ActionTypeId = record.ActionTypeId;

            if (record.EndDate != null) oldTournament.EndDate = record.EndDate;
            if (record.StartDate != null) oldTournament.StartDate = record.StartDate;
            if (record.NumberOfParticipants != null && record.NumberOfParticipants > 0) oldTournament.NumberOfParticipants = record.NumberOfParticipants;
            if (!string.IsNullOrWhiteSpace(record.UserHostId))
            {
                oldTournament.UserHostId = record.UserHostId;
            }
            if (record.Public != null) oldTournament.Public = record.Public;
            if (!string.IsNullOrWhiteSpace(record.LeaderBoardId)) oldTournament.LeaderBoardId = record.LeaderBoardId;

            if (!string.IsNullOrWhiteSpace(record.PriceId)) oldTournament.PriceId = record.PriceId;
            if (record.TypeId != null) oldTournament.TypeId = record.TypeId;


            //    Imageurl = c.Imageurl
            //upload
            if (record.FormImage != null)
            {
                var allowedExtensions = new[] { ".jpg", ".JPG", ".jpeg", ".JPEG", ".png", ".PNG" };
                var extension = Path.GetExtension(record.FormImage.FileName);
                if (allowedExtensions.Contains(extension))
                {
                    var file = record.FormImage.OpenReadStream();
                    var fileName = record.FormImage.FileName;
                    if (file.Length > 0)
                    {
                        var newFileName = Guid.NewGuid().ToString() + "-" + fileName;
                        var physicalPath = string.Format(TournamentPath, Directory.GetCurrentDirectory() + "/wwwroot", newFileName);
                        string dirPath = Path.GetDirectoryName(physicalPath);

                        if (!Directory.Exists(dirPath))
                            Directory.CreateDirectory(dirPath);
                        var virtualPath = string.Format(TournamentPath, baseUrl, newFileName);

                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        oldTournament.ImageUrl = virtualPath;
                    }
                }
            }

            return oldTournament;
        }

        public static IQueryable<TournamentRecord> ApplyFilter(IQueryable<TournamentRecord> query,
            TournamentRecord tournamentRecord)
        {
            if (tournamentRecord.Id > 0)
                query = query.Where(c => c.Id == tournamentRecord.Id);
            //if (tournamentRecord.Valid != null && tournamentRecord.Valid.Value == true)
            //    query = query.Where(c => c.Validfrom != null && c.Validfrom.Value.Date <= DateTime.UtcNow.Date
            //    && c.Validto != null && c.Validto.Value.Date >= DateTime.UtcNow.Date && c.Status != null && c.Status.Value == true
            //    && c.Usedcount <= c.Maxusagecount);

            if (!string.IsNullOrWhiteSpace(tournamentRecord.ObjectTypeId))
                query = query.Where(c =>
                    c.ObjectTypeId != null && c.ObjectTypeId.Trim().Contains(tournamentRecord.ObjectTypeId.Trim()));

            return query;
        }

        private static string GetCodeResult()
        {
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }
    }
}