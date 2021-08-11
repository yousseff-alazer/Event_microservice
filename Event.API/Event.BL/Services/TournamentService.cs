using Event.BL.Services.Managers;
using Event.CommonDefinitions.Records;
using Event.CommonDefinitions.Requests;
using Event.CommonDefinitions.Responses;
using Event.Helpers;
using System;
using System.Linq;
using System.Net;

namespace Event.BL.Services
{
    public class TournamentService : BaseService
    {
        public static TournamentResponse ListTournament(TournamentRequest request)
        {
            var res = new TournamentResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.Tournaments.Where(c => !c.IsDeleted.Value).Select(c =>
                        new TournamentRecord
                        {
                            Id = c.Id,
                            CreationDate = c.CreationDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedBy = c.ModifiedBy,
                            ModificationDate = c.ModificationDate,
                            Name = c.Name,
                            Description = c.Description,
                            Code = c.Code,
                            EndDate = c.EndDate,
                            StartDate = c.StartDate,
                            ImageUrl = c.ImageUrl,
                            NumberOfParticipants = c.NumberOfParticipants,
                            UserHostId = c.UserHostId,
                            Public = c.Public,
                            ObjectTypeId = c.ObjectTypeId,
                            ObjectId = c.ObjectId,
                            ActionTypeId = c.ActionTypeId,
                            LeaderBoardId = c.LeaderBoardId,
                            PriceId = c.PriceId,
                            TypeId = c.TypeId,
                            TypeName = c.Type != null ? c.Type.Name : "",
                            Translates = !string.IsNullOrWhiteSpace(request.LanguageId) &&
                                         c.TournamentTranslates != null
                                ? c.TournamentTranslates.Where(t => t.LanguageId == request.LanguageId).ToList()
                                : null
                        });

                    if (request.TournamentRecord != null)
                        query = TournamentServiceManager.ApplyFilter(query, request.TournamentRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.TournamentRecords = query.ToList();
                    res.Message = HttpStatusCode.OK.ToString();
                    res.Success = true;
                    res.StatusCode = HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message;
                    res.Success = false;
                    LogHelper.LogException(ex.Message, ex.StackTrace);
                }

                return res;
            });
            return res;
        }

        public static TournamentResponse DeleteTournament(TournamentRequest request)
        {
            var res = new TournamentResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TournamentRecord;
                    var tournament =
                        request._context.Tournaments.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (tournament != null)
                    {
                        //update tournament IsDeleted
                        tournament.IsDeleted = true;
                        tournament.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid tournament";
                        res.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message;
                    res.Success = false;
                    LogHelper.LogException(ex.Message, ex.StackTrace);
                }

                return res;
            });
            return res;
        }

        public static TournamentResponse EditTournament(TournamentRequest request)
        {
            var res = new TournamentResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TournamentRecord;
                    var tournament = request._context.Tournaments.Find(model.Id);
                    if (tournament != null)
                    {
                        //update whole tournament
                        tournament = TournamentServiceManager.AddOrEditTournament(request.BaseUrl,
                            request.TournamentRecord, tournament);
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid tournament";
                        res.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message;
                    res.Success = false;
                    LogHelper.LogException(ex.Message, ex.StackTrace);
                }

                return res;
            });
            return res;
        }

        public static TournamentResponse AddTournament(TournamentRequest request)
        {
            var res = new TournamentResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var TournamentExist = request._context.Tournaments.Any(m =>
                        m.Name.ToLower() == request.TournamentRecord.Name.ToLower() && !m.IsDeleted.Value);
                    if (!TournamentExist)
                    {
                        var tournament =
                            TournamentServiceManager.AddOrEditTournament(request.BaseUrl, request.TournamentRecord);
                        request._context.Tournaments.Add(tournament);
                        request._context.SaveChanges();
                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Tournament already exist";
                        res.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message;
                    res.Success = false;
                    LogHelper.LogException(ex.Message, ex.StackTrace);
                }

                return res;
            });
            return res;
        }
    }
}