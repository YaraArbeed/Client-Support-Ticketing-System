using Microsoft.AspNetCore.Mvc;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _TicketService;

        public TicketController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        [HttpGet("Get all Tickets")]
        public async Task<IActionResult> GetTicketList()
        {
            var ticketResponses = await _TicketService.GetTicketListAsync();

            return Ok(ticketResponses);
        }
    }
}
