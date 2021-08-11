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
    public class TypeService : BaseService
    {
        public static TypeResponse ListType(TypeRequest request)
        {
            var res = new TypeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var query = request._context.Types.Where(c => !c.IsDeleted.Value).Select(c => new TypeRecord
                    {
                        Id = c.Id,
                        CreationDate = c.CreationDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedBy = c.ModifiedBy,
                        ModificationDate = c.ModificationDate,
                        Name = c.Name
                    });

                    if (request.TypeRecord != null)
                        query = TypeServiceManager.ApplyFilter(query, request.TypeRecord);

                    res.TotalCount = query.Count();

                    query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    res.TypeRecords = query.ToList();
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

        public static TypeResponse DeleteType(TypeRequest request)
        {
            var res = new TypeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TypeRecord;
                    var type = request._context.Types.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (type != null)
                    {
                        //update type IsDeleted
                        type.IsDeleted = true;
                        type.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid type";
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

        public static TypeResponse EditType(TypeRequest request)
        {
            var res = new TypeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TypeRecord;
                    var type = request._context.Types.Find(model.Id);
                    if (type != null)
                    {
                        //update whole type
                        type = TypeServiceManager.AddOrEditType(request.BaseUrl, request.TypeRecord, type);
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid type";
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

        public static TypeResponse AddType(TypeRequest request)
        {
            var res = new TypeResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var TypeExist = request._context.Types.Any(m =>
                        m.Name.ToLower() == request.TypeRecord.Name.ToLower() && !m.IsDeleted.Value);
                    if (!TypeExist)
                    {
                        var type = TypeServiceManager.AddOrEditType(request.BaseUrl, request.TypeRecord);
                        request._context.Types.Add(type);
                        request._context.SaveChanges();
                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Type already exist";
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