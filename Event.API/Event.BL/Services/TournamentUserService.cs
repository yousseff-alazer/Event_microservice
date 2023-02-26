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
    public class TournamentUserService : BaseService
    {
        public static TournamentUserResponse ListTournamentUser(TournamentUserRequest request)
        {
            var res = new TournamentUserResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var query = request._context.TournamentUsers.Where(c => !c.IsDeleted.Value).Select(c => new TournamentUserRecord
                    {
                        Id = c.Id,
                        CreationDate = c.CreationDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedBy = c.ModifiedBy,
                        ModificationDate = c.ModificationDate,
                        TournamentId = c.TournamentId,
                        UserId = c.UserId
                    });

                    if (request.TournamentUserRecord != null)
                        query = TournamentUserServiceManager.ApplyFilter(query, request.TournamentUserRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.TournamentUserRecords = query.ToList();
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

        public static TournamentUserResponse DeleteTournamentUser(TournamentUserRequest request)
        {
            var res = new TournamentUserResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TournamentUserRecord;
                    var tournamentUser = request._context.TournamentUsers.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (tournamentUser != null)
                    {
                        //update tournamentUser IsDeleted
                        tournamentUser.IsDeleted = true;
                        tournamentUser.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid tournamentUser";
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

        public static TournamentUserResponse EditTournamentUser(TournamentUserRequest request)
        {
            var res = new TournamentUserResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TournamentUserRecord;
                    var tournamentUser = request._context.TournamentUsers.Find(model.Id);
                    if (tournamentUser != null)
                    {
                        //update whole tournamentUser
                        tournamentUser = TournamentUserServiceManager.AddOrEditTournamentUser(request.BaseUrl, request.TournamentUserRecord, tournamentUser);
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid tournamentUser";
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

        public static TournamentUserResponse AddTournamentUser(TournamentUserRequest request)
        {
            var res = new TournamentUserResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var TournamentUserExist = request._context.TournamentUsers.Any(m =>
                        m.UserId.ToLower() == request.TournamentUserRecord.UserId.ToLower() && m.TournamentId == request.TournamentUserRecord.TournamentId && !m.IsDeleted.Value);
                    if (!TournamentUserExist)
                    {
                        var tournamentUser = TournamentUserServiceManager.AddOrEditTournamentUser(request.BaseUrl, request.TournamentUserRecord);
                        request._context.TournamentUsers.Add(tournamentUser);
                        request._context.SaveChanges();
                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "TournamentUser already exist";
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