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

        public static Tournament AddOrEditTournament(string baseUrl /*, long createdBy*/, TournamentRecord record,
            Tournament oldTournament = null)
        {
            if (oldTournament == null) //new tournament
            {
                oldTournament = new Tournament();
                oldTournament.CreationDate = DateTime.Now;
                oldTournament.CreatedBy = record.CreatedBy;
            }
            else
            {
                oldTournament.ModificationDate = DateTime.Now;
                oldTournament.ModifiedBy = record.CreatedBy;
            }

            if (!string.IsNullOrWhiteSpace(record.Name)) oldTournament.Name = record.Name;
            if (!string.IsNullOrWhiteSpace(record.Description)) oldTournament.Description = record.Description;

            if (record.ObjectId != null) oldTournament.ObjectId = record.ObjectId;
            if (record.ObjectTypeId != null) oldTournament.ObjectTypeId = record.ObjectTypeId;
            if (record.ActionTypeId != null) oldTournament.ActionTypeId = record.ActionTypeId;
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
                        var newFileName = Guid.NewGuid() + "-" + fileName;
                        var physicalPath = string.Format(TournamentPath, Directory.GetCurrentDirectory() + "/wwwroot",
                            newFileName);
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
    }
}