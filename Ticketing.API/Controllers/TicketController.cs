using Microsoft.AspNetCore.Mvc;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Implementation;
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

        [HttpGet("Get All Tickets For Specific User")]
        public async Task<IActionResult> GetTicketListById(int id)
        {
            var ticketResponses = await _TicketService.GetTicketListByIdAsync(id);
            return Ok(ticketResponses);
        }

        [HttpPost("Add Ticket")]
        public async Task<IActionResult> AddTicket([FromBody] TicketParam param)
        {

            var ticket = await _TicketService.AddTicketAsync(param);
            return Ok(ticket);
        }

        [HttpGet("View Ticket/{id}")]
        public async Task<IActionResult> ViewTicket(int id)
        {

            var ticket = await _TicketService.ViewTicketAsync(id);
            return Ok(ticket);
        }

        [HttpPut("Edit Ticket/{id}")]
        public async Task<IActionResult> EditTicket(int id, [FromBody] TicketEditParam Param)
        {
            var ticket = await _TicketService.EditTicketAsync(id,Param);
            return Ok(ticket);
        }
    }
}
