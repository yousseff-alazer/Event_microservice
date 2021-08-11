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
    public class TournamentTranslateService : BaseService
    {
        public static TournamentTranslateResponse ListTournamentTranslate(TournamentTranslateRequest request)
        {
            var res = new TournamentTranslateResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.TournamentTranslates.Where(c => !c.IsDeleted.Value).Select(c =>
                        new TournamentTranslateRecord
                        {
                            Id = c.Id,
                            CreationDate = c.CreationDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedBy = c.ModifiedBy,
                            ModificationDate = c.ModificationDate,
                            Name = c.Name,
                            Description = c.Description,
                            TournamentId = c.TournamentId,
                            ImageUrl = c.ImageUrl,
                            LanguageId = c.LanguageId
                        });

                    if (request.TournamentTranslateRecord != null)
                        query = TournamentTranslateServiceManager.ApplyFilter(query, request.TournamentTranslateRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.TournamentTranslateRecords = query.ToList();
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

        public static TournamentTranslateResponse DeleteTournamentTranslate(TournamentTranslateRequest request)
        {
            var res = new TournamentTranslateResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TournamentTranslateRecord;
                    var tournamentTranslate =
                        request._context.TournamentTranslates.FirstOrDefault(
                            c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (tournamentTranslate != null)
                    {
                        //update tournamentTranslate IsDeleted
                        tournamentTranslate.IsDeleted = true;
                        tournamentTranslate.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid tournamentTranslate";
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

        public static TournamentTranslateResponse EditTournamentTranslate(TournamentTranslateRequest request)
        {
            var res = new TournamentTranslateResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TournamentTranslateRecord;
                    var tournamentTranslate = request._context.TournamentTranslates.Find(model.Id);
                    if (tournamentTranslate != null)
                    {
                        //update whole tournamentTranslate
                        tournamentTranslate =
                            TournamentTranslateServiceManager.AddOrEditTournamentTranslate(request.BaseUrl,
                                request.TournamentTranslateRecord, tournamentTranslate);
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid tournamentTranslate";
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

        public static TournamentTranslateResponse AddTournamentTranslate(TournamentTranslateRequest request)
        {
            var res = new TournamentTranslateResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var TournamentTranslateExist = request._context.TournamentTranslates.Any(m =>
                        m.Name.ToLower() == request.TournamentTranslateRecord.Name.ToLower() && !m.IsDeleted.Value);
                    if (!TournamentTranslateExist)
                    {
                        var tournamentTranslate =
                            TournamentTranslateServiceManager.AddOrEditTournamentTranslate(request.BaseUrl,
                                request.TournamentTranslateRecord);
                        request._context.TournamentTranslates.Add(tournamentTranslate);
                        request._context.SaveChanges();
                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "TournamentTranslate already exist";
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