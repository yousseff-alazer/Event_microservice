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
    public class TypeTranslateService : BaseService
    {
        public static TypeTranslateResponse ListTypeTranslate(TypeTranslateRequest request)
        {
            var res = new TypeTranslateResponse();
            RunBase(request, res, req =>

            {
                try
                {
                    var query = request._context.TypeTranslates.Where(c => !c.IsDeleted.Value).Select(c =>
                        new TypeTranslateRecord
                        {
                            Id = c.Id,
                            CreationDate = c.CreationDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedBy = c.ModifiedBy,
                            ModificationDate = c.ModificationDate,
                            Name = c.Name,
                            LanguageId = c.LanguageId,
                            TypeId = c.TypeId
                        });

                    if (request.TypeTranslateRecord != null)
                        query = TypeTranslateServiceManager.ApplyFilter(query, request.TypeTranslateRecord);

                    res.TotalCount = query.Count();

                    query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                    if (request.PageSize > 0)
                        query = ApplyPaging(query, request.PageSize, request.PageIndex);

                    res.TypeTranslateRecords = query.ToList();
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

        public static TypeTranslateResponse DeleteTypeTranslate(TypeTranslateRequest request)
        {
            var res = new TypeTranslateResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    var model = request.TypeTranslateRecord;
                    var typeTranslate =
                        request._context.TypeTranslates.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                    if (typeTranslate != null)
                    {
                        //update typeTranslate IsDeleted
                        typeTranslate.IsDeleted = true;
                        typeTranslate.ModificationDate = DateTime.Now;
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid typeTranslate";
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

        public static TypeTranslateResponse EditTypeTranslate(TypeTranslateRequest request)
        {
            var res = new TypeTranslateResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.TypeTranslateRecords)
                    {
                        var typeTranslate = request._context.TypeTranslates.Find(model.Id);
                        if (typeTranslate != null)
                        {
                            //update whole typeTranslate
                            typeTranslate = TypeTranslateServiceManager.AddOrEditTypeTranslate(request.BaseUrl,
                                model, typeTranslate);
                            request._context.SaveChanges();

                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "Invalid typeTranslate";
                            res.Success = false;
                        }
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

        public static TypeTranslateResponse AddTypeTranslate(TypeTranslateRequest request)
        {
            var res = new TypeTranslateResponse();
            RunBase(request, res, req =>
            {
                try
                {
                    foreach (var model in req.TypeTranslateRecords)
                    {
                        var TypeTranslateExist = request._context.TypeTranslates.Any(m =>
                            m.Name.ToLower() == model.Name.ToLower() && !m.IsDeleted.Value && m.TypeId == model.TypeId&&m.LanguageId==model.LanguageId);
                        if (!TypeTranslateExist)
                        {
                            var typeTranslate =
                                TypeTranslateServiceManager.AddOrEditTypeTranslate(request.BaseUrl,
                                    model);
                            request._context.TypeTranslates.Add(typeTranslate);
                            request._context.SaveChanges();
                            res.Message = HttpStatusCode.OK.ToString();
                            res.Success = true;
                            res.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            res.Message = "TypeTranslate already exist";
                            res.Success = false;
                        }
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