using System;
using Event.API.Event.DAL.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event.BL.Services;
using Event.CommonDefinitions.Records;
using Event.CommonDefinitions.Requests;
using Event.CommonDefinitions.Responses;
using Event.Helpers;
using Type = Event.API.Event.DAL.DB.Type;

namespace Event.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentTypesController : ControllerBase
    {
        private readonly eventdbContext _context;

        public TournamentTypesController(eventdbContext context)
        {
            _context = context;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type>>> GetTypes()
        {
            return await _context.Types.ToListAsync();
        }

        [HttpGet]
        [Route("GetAll")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var typeResponse = new TypeResponse();
            try
            {
                var typeRequest = new TypeRequest
                {
                    _context = _context
                };
                typeResponse = TypeService.ListType(typeRequest);
            }
            catch (Exception ex)
            {
                typeResponse.Message = ex.Message;
                typeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(typeResponse);
        }

        /// <summary>
        /// Return Type With id .
        /// </summary>
        [HttpGet("{id}", Name = "GetType")]
        [Produces("application/json")]
        public IActionResult GetType(int id)
        {
            var typeResponse = new TypeResponse();
            try
            {
                var typeRequest = new TypeRequest
                {
                    _context = _context,
                    TypeRecord = new TypeRecord
                    {
                        Id = id
                    }
                };
                typeResponse = TypeService.ListType(typeRequest);
            }
            catch (Exception ex)
            {
                typeResponse.Message = ex.Message;
                typeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(typeResponse);
        }

        /// <summary>
        /// Return List Of Types With filter valid and any  needed filter like id,name,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] TypeRequest model)
        {
            var typeResponse = new TypeResponse();
            try
            {
                if (model == null)
                {
                    model = new TypeRequest();
                }
                model._context = _context;

                typeResponse = TypeService.ListType(model);
            }
            catch (Exception ex)
            {
                typeResponse.Message = ex.Message;
                typeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(typeResponse);
        }

        /// <summary>
        /// Creates Type, Uncheck Send empty value in Id,Creationdate,Isdeleted,IsDesc,PageSize,PageIndex.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        [Produces("application/json")]
        public IActionResult Add([FromBody] TypeRequest model)
        {
            var typeResponse = new TypeResponse();
            try
            {
                if (model == null)
                {
                    typeResponse.Message = "Empty Body";
                    typeResponse.Success = false;
                    return Ok(typeResponse);
                }

                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                typeResponse = TypeService.AddType(model);
            }
            catch (Exception ex)
            {
                typeResponse.Message = ex.Message;
                typeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(typeResponse);
        }

        /// <summary>
        /// Update Type , Uncheck Send empty value in Id,Creationdate,Isdeleted,IsDesc,PageSize,PageIndex.
        /// </summary>
        [HttpPost]
        [Route("Edit")]
        [Produces("application/json")]
        public IActionResult Edit([FromBody] TypeRequest model)
        {
            var typeResponse = new TypeResponse();
            try
            {
                if (model == null)
                {
                    typeResponse.Message = "Empty Body";
                    typeResponse.Success = false;
                    return Ok(typeResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                typeResponse = TypeService.EditType(model);
            }
            catch (Exception ex)
            {
                typeResponse.Message = ex.Message;
                typeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(typeResponse);
        }

        /// <summary>
        /// Remove Type .
        /// </summary>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public IActionResult Delete([FromBody] TypeRequest model)
        {
            var typeResponse = new TypeResponse();
            try
            {
                if (model == null)
                {
                    typeResponse.Message = "Empty Body";
                    typeResponse.Success = false;
                    return Ok(typeResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                typeResponse = TypeService.DeleteType(model);
            }
            catch (Exception ex)
            {
                typeResponse.Message = ex.Message;
                typeResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(typeResponse);
        }

        /// <summary>
        /// Return Type With id .
        /// </summary>
        [HttpGet("GetTypeTranslate/{Typeid}", Name = "GetTypeTranslate")]
        [Produces("application/json")]
        public IActionResult GetTypeTranslate(int Typeid)
        {
            var typeTranslateResponse = new TypeTranslateResponse();
            try
            {
                var typeTranslateRequest = new TypeTranslateRequest
                {
                    _context = _context,
                    TypeTranslateRecord = new TypeTranslateRecord
                    {
                        TypeId = Typeid
                    }
                };
                typeTranslateResponse = TypeTranslateService.ListTypeTranslate(typeTranslateRequest);
            }
            catch (Exception ex)
            {
                typeTranslateResponse.Message = ex.Message;
                typeTranslateResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(typeTranslateResponse);
        }

        /// <summary>
        /// Creates TypeTranslate, Uncheck Send empty value in Id,Creationdate,Isdeleted,IsDesc,PageSize,PageIndex.
        /// </summary>
        [HttpPost]
        [Route("AddTypeTranslate")]
        [Produces("application/json")]
        public IActionResult AddTypeTranslate([FromBody] TypeTranslateRequest model)
        {
            var typeTranslateResponse = new TypeTranslateResponse();
            try
            {
                if (model == null)
                {
                    typeTranslateResponse.Message = "Empty Body";
                    typeTranslateResponse.Success = false;
                    return Ok(typeTranslateResponse);
                }

                    var editedTranslateType = model.TypeTranslateRecords.Where(c => c.Id > 0).ToList();
                    var editReq = new TypeTranslateRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        TypeTranslateRecords = editedTranslateType
                    };
                    typeTranslateResponse = TypeTranslateService.EditTypeTranslate(editReq);
                    var addedTranslateType = model.TypeTranslateRecords.Where(c => c.Id == 0).ToList();
                    var addReq = new TypeTranslateRequest
                    {
                        _context = _context,
                        BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                        TypeTranslateRecords = addedTranslateType
                    };
                    typeTranslateResponse = TypeTranslateService.AddTypeTranslate(addReq);
            }
            catch (Exception ex)
            {
                typeTranslateResponse.Message = ex.Message;
                typeTranslateResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(typeTranslateResponse);
        }

        //[HttpGet]
        //[Route("CreateCoupon")]
        //[Produces("application/json")]
        //public IActionResult CreateCoupon()
        //{
        //    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    var random = new Random();
        //    var result = new string(
        //        Enumerable.Repeat(chars, 20)
        //                  .Select(s => s[random.Next(s.Length)])
        //                  .ToArray());
        //    return Ok(result);
        //}
    }
}