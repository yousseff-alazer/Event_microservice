//using Event.API.Event.DAL.DB;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Event.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TournamentsController : ControllerBase
//    {
//        private readonly eventdbContext _context;

//        public TournamentsController(eventdbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Tournaments
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
//        {
//            return await _context.Tournaments.ToListAsync();
//        }
//    }
//}