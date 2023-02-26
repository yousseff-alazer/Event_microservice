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
using Tournament = Event.API.Event.DAL.DB.Tournament;

namespace Event.API.Controllers
{
    [Route("api/event/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly eventdbContext _context;

        public TournamentController(eventdbContext context)
        {
            _context = context;
        }

        // GET: api/Tournament
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament()
        {
            return await _context.Tournaments.ToListAsync();
        }


        [HttpGet]
        [Route("GetAll")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var tournamentResponse = new TournamentResponse();
            try
            {
                var tournamentRequest = new TournamentRequest
                {
                    _context = _context
                };
                tournamentResponse = TournamentService.ListTournament(tournamentRequest);
            }
            catch (Exception ex)
            {
                tournamentResponse.Message = ex.Message;
                tournamentResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(tournamentResponse);
        }

        /// <summary>
        /// Return Tournament With id .
        /// </summary>
        [HttpGet("{id}", Name = "GetTournament")]
        [Produces("application/json")]
        public IActionResult GetTournament(int id)
        {
            var tournamentResponse = new TournamentResponse();
            try
            {
                var tournamentRequest = new TournamentRequest
                {
                    _context = _context,
                    TournamentRecord = new TournamentRecord
                    {
                        Id = id
                    }
                };
                tournamentResponse = TournamentService.ListTournament(tournamentRequest);
            }
            catch (Exception ex)
            {
                tournamentResponse.Message = ex.Message;
                tournamentResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(tournamentResponse);
        }

        /// <summary>
        /// Return List Of Tournament With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] TournamentRequest model)
        {
            var tournamentResponse = new TournamentResponse();
            try
            {
                if (model == null)
                {
                    model = new TournamentRequest();
                }
                model._context = _context;

                tournamentResponse = TournamentService.ListTournament(model);
            }
            catch (Exception ex)
            {
                tournamentResponse.Message = ex.Message;
                tournamentResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(tournamentResponse);
        }

        /// <summary>
        /// Creates Tournament, Uncheck Send empty value in Id,Creationdate,Isdeleted,IsDesc,PageSize,PageIndex.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        [Produces("application/json")]
        public IActionResult Add([FromForm] TournamentRequest model)
        {
            var tournamentResponse = new TournamentResponse();
            try
            {
                if (model == null||model.TournamentRecord==null)
                {
                    tournamentResponse.Message = "Empty Body";
                    tournamentResponse.Success = false;
                    return Ok(tournamentResponse);
                }

                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                tournamentResponse = TournamentService.AddTournament(model);
            }
            catch (Exception ex)
            {
                tournamentResponse.Message = ex.Message;
                tournamentResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
                return Ok(tournamentResponse);
            }

            return Ok(tournamentResponse);
        }

        /// <summary>
        /// Update Tournament , Uncheck Send empty value in Id,Creationdate,Isdeleted,IsDesc,PageSize,PageIndex.
        /// </summary>
        [HttpPost]
        [Route("Edit")]
        [Produces("application/json")]
        public IActionResult Edit([FromForm] TournamentRequest model)
        {
            var tournamentResponse = new TournamentResponse();
            try
            {
                if (model == null)
                {
                    tournamentResponse.Message = "Empty Body";
                    tournamentResponse.Success = false;
                    return Ok(tournamentResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                tournamentResponse = TournamentService.EditTournament(model);
            }
            catch (Exception ex)
            {
                tournamentResponse.Message = ex.Message;
                tournamentResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(tournamentResponse);
        }

        /// <summary>
        /// Remove Tournament .
        /// </summary>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public IActionResult Delete([FromBody] TournamentRequest model)
        {
            var tournamentResponse = new TournamentResponse();
            try
            {
                if (model == null)
                {
                    tournamentResponse.Message = "Empty Body";
                    tournamentResponse.Success = false;
                    return Ok(tournamentResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                tournamentResponse = TournamentService.DeleteTournament(model);
            }
            catch (Exception ex)
            {
                tournamentResponse.Message = ex.Message;
                tournamentResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(tournamentResponse);
        }

        /// <summary>
        /// Return Tournament With id .
        /// </summary>
        [HttpGet("GetTournamentTranslate/{Tournamentid}", Name = "GetTournamentTranslate")]
        [Produces("application/json")]
        public IActionResult GetTournamentTranslate(int Tournamentid)
        {
            var tournamentTranslateResponse = new TournamentTranslateResponse();
            try
            {
                var tournamentTranslateRequest = new TournamentTranslateRequest
                {
                    _context = _context,
                    TournamentTranslateRecord = new TournamentTranslateRecord
                    {
                        TournamentId = Tournamentid
                    }
                };
                tournamentTranslateResponse = TournamentTranslateService.ListTournamentTranslate(tournamentTranslateRequest);
            }
            catch (Exception ex)
            {
                tournamentTranslateResponse.Message = ex.Message;
                tournamentTranslateResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(tournamentTranslateResponse);
        }

        /// <summary>
        /// Creates TournamentTranslate.
        /// </summary>
        [HttpPost]
        [Route("AddTournamentTranslate")]
        [Produces("application/json")]
        public IActionResult AddTournamentTranslate([FromBody] TournamentTranslateRequest model)
        {
            var tournamentTranslateResponse = new TournamentTranslateResponse();
            try
            {
                if (model == null)
                {
                    tournamentTranslateResponse.Message = "Empty Body";
                    tournamentTranslateResponse.Success = false;
                    return Ok(tournamentTranslateResponse);
                }

                var editedTranslateTournament = model.TournamentTranslateRecords.Where(c => c.Id > 0).ToList();
                var editReq = new TournamentTranslateRequest
                {
                    _context = _context,
                    BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                    TournamentTranslateRecords = editedTranslateTournament
                };
                tournamentTranslateResponse = TournamentTranslateService.EditTournamentTranslate(editReq);
                var addedTranslateTournament = model.TournamentTranslateRecords.Where(c => c.Id == 0).ToList();
                var addReq = new TournamentTranslateRequest
                {
                    _context = _context,
                    BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                    TournamentTranslateRecords = addedTranslateTournament
                };
                tournamentTranslateResponse = TournamentTranslateService.AddTournamentTranslate(addReq);
            }
            catch (Exception ex)
            {
                tournamentTranslateResponse.Message = ex.Message;
                tournamentTranslateResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(tournamentTranslateResponse);
        }



        /// <summary>
        /// Join Tournament.
        /// </summary>
        [HttpGet("JoinTournament/{tournamentid}/{userid}", Name = "JoinTournament")]
        [Produces("application/json")]
        public IActionResult JoinTournament(int tournamentid, string userid)
        {
            var tournamentResponse = new TournamentUserResponse();
            try
            {
                if (tournamentid == 0 || !string.IsNullOrWhiteSpace(userid))
                {
                    tournamentResponse.Message = "Wrong Input";
                    tournamentResponse.Success = false;
                    return Ok(tournamentResponse);
                }
                var tournamentUserReq = new TournamentUserRequest()
                {
                    _context = _context,
                    TournamentUserRecord = new TournamentUserRecord()
                    {
                        UserId = userid,
                        TournamentId = tournamentid
                    }
                };
                tournamentResponse = TournamentUserService.AddTournamentUser(tournamentUserReq);
            }
            catch (Exception ex)
            {
                tournamentResponse.Message = ex.Message;
                tournamentResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(tournamentResponse);
        }


        /// <summary>
        /// Remove TournamentTranslate .
        /// </summary>
        [HttpPost]
        [Route("DeleteTournamentTranslate")]
        [Produces("application/json")]
        public IActionResult DeleteTournamentTranslate([FromBody] TournamentTranslateRequest model)
        {
            var tournamentTranslateResponse = new TournamentTranslateResponse();
            try
            {
                if (model == null)
                {
                    tournamentTranslateResponse.Message = "Empty Body";
                    tournamentTranslateResponse.Success = false;
                    return Ok(tournamentTranslateResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                tournamentTranslateResponse = TournamentTranslateService.DeleteTournamentTranslate(model);
            }
            catch (Exception ex)
            {
                tournamentTranslateResponse.Message = ex.Message;
                tournamentTranslateResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(tournamentTranslateResponse);
        }
    }
}