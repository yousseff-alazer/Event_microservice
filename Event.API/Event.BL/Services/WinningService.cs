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
    public class WinningService : BaseService
    {
        public static WinningResponse ListWinning(WinningRequest request)
        {
            var res = new WinningResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.Winnings.Where(c => !c.IsDeleted.Value).Select(c => new WinningRecord
                    {
                        Id = c.Id,
                        CreationDate = c.CreationDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedBy = c.ModifiedBy,
                        ModificationDate = c.ModificationDate,
                        Order = c.Order,
                        Amount = c.Amount,
                        PriceTypeId = c.PriceTypeId,
                        TournamentId = c.TournamentId
                    });

                    if (request.WinningRecord != null)
                        query = WinningServiceManager.ApplyFilter(query, request.WinningRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.WinningRecords = query.ToList();
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

        public static WinningResponse DeleteWinning(WinningRequest request)
        {
            var res = new WinningResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.WinningRecord;
                    var winning = request._context.Winnings.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (winning != null)
                    {
                        //update winning IsDeleted
                        winning.IsDeleted = true;
                        winning.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid winning";
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

        public static WinningResponse EditWinning(WinningRequest request)
        {
            var res = new WinningResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.WinningRecord;
                    var winning = request._context.Winnings.Find(model.Id);
                    if (winning != null)
                    {
                        //update whole winning
                        winning = WinningServiceManager.AddOrEditWinning(request.BaseUrl, request.WinningRecord,
                            winning);
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid winning";
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

        //public static WinningResponse AddWinning(WinningRequest request)
        //{
        //    var res = new WinningResponse();
        //    RunBase(request, res, (WinningRequest req) =>
        //    {
        //        try
        //        {
        //            var WinningExist = request._context.Winnings.Any(m => m.Order.ToLower() == request.WinningRecord.Name.ToLower() && !m.IsDeleted.Value);
        //            if (!WinningExist)
        //            {
        //                var winning = WinningServiceManager.AddOrEditWinning(request.BaseUrl, request.WinningRecord);
        //                request._context.Winnings.Add(winning);
        //                request._context.SaveChanges();
        //                res.Message = HttpStatusCode.OK.ToString();
        //                res.Success = true;
        //                res.StatusCode = HttpStatusCode.OK;
        //            }
        //            else
        //            {
        //                res.Message = "Winning already exist";
        //                res.Success = false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            res.Message = ex.Message;
        //            res.Success = false;
        //            LogHelper.LogException(ex.Message, ex.StackTrace);
        //        }
        //        return res;
        //    });
        //    return res;
        //}
    }
}