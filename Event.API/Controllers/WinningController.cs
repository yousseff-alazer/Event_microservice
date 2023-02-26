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
using Winning = Event.API.Event.DAL.DB.Winning;

namespace Event.API.Controllers
{
    [Route("api/event/[controller]")]
    [ApiController]
    public class WinningController : ControllerBase
    {
        private readonly eventdbContext _context;

        public WinningController(eventdbContext context)
        {
            _context = context;
        }

        //// GET: api/Winning
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Winning>>> GetWinning()
        //{
        //    return await _context.Winnings.ToListAsync();
        //}


        [HttpGet]
        [Route("GetAll")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var winningResponse = new WinningResponse();
            try
            {
                var winningRequest = new WinningRequest
                {
                    _context = _context
                };
                winningResponse = WinningService.ListWinning(winningRequest);
            }
            catch (Exception ex)
            {
                winningResponse.Message = ex.Message;
                winningResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(winningResponse);
        }
        /// <summary>
        /// Return List Of Winning With filter valid and any  needed filter like id,...  .
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] WinningRequest model)
        {
            var winningResponse = new WinningResponse();
            try
            {
                if (model == null)
                {
                    model = new WinningRequest();
                }
                model._context = _context;

                winningResponse = WinningService.ListWinning(model);
            }
            catch (Exception ex)
            {
                winningResponse.Message = ex.Message;
                winningResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(winningResponse);
        }
        /// <summary>
        /// Return Type With id .
        /// </summary>
        [HttpGet("GetWinning/{Tournamentid}", Name = "GetWinning")]
        [Produces("application/json")]
        public IActionResult GetWinning(int Tournamentid)
        {
            var winningResponse = new WinningResponse();
            try
            {
                var winningRequest = new WinningRequest
                {
                    _context = _context,
                    WinningRecord = new WinningRecord
                    {
                        TournamentId = Tournamentid
                    }
                };
                winningResponse = WinningService.ListWinning(winningRequest);
            }
            catch (Exception ex)
            {
                winningResponse.Message = ex.Message;
                winningResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }
            return Ok(winningResponse);
        }

        /// <summary>
        /// Creates Winning.
        /// </summary>
        [HttpPost]
        [Route("AddWinning")]
        [Produces("application/json")]
        public IActionResult AddWinning([FromBody] WinningRequest model)
        {
            var winningResponse = new WinningResponse();
            try
            {
                if (model == null)
                {
                    winningResponse.Message = "Empty Body";
                    winningResponse.Success = false;
                    return Ok(winningResponse);
                }

                var editedTranslateType = model.WinningRecords.Where(c => c.Id > 0).ToList();
                var editReq = new WinningRequest
                {
                    _context = _context,
                    BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                    WinningRecords = editedTranslateType
                };
                winningResponse = WinningService.EditWinning(editReq);
                var addedTranslateType = model.WinningRecords.Where(c => c.Id == 0).ToList();
                var addReq = new WinningRequest
                {
                    _context = _context,
                    BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase,
                    WinningRecords = addedTranslateType
                };
                winningResponse = WinningService.AddWinning(addReq);
            }
            catch (Exception ex)
            {
                winningResponse.Message = ex.Message;
                winningResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(winningResponse);
        }

        /// <summary>
        /// Remove Winning .
        /// </summary>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public IActionResult Delete([FromBody] WinningRequest model)
        {
            var winningResponse = new WinningResponse();
            try
            {
                if (model == null)
                {
                    winningResponse.Message = "Empty Body";
                    winningResponse.Success = false;
                    return Ok(winningResponse);
                }
                model._context = _context;
                model.BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
                winningResponse = WinningService.DeleteWinning(model);
            }
            catch (Exception ex)
            {
                winningResponse.Message = ex.Message;
                winningResponse.Success = false;
                LogHelper.LogException(ex.Message, ex.StackTrace);
            }

            return Ok(winningResponse);
        }
    }
}