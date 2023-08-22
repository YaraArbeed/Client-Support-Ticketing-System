using Microsoft.AspNetCore.Mvc;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    public class ManagerController : ControllerBase
    {
        private readonly ITicketService _TicketService;

        public ManagerController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        [HttpGet("Get all Tickets")]
        public async Task<IActionResult> GetTicketList()
        {
            var TicketResponses = await _TicketService.GetTicketListAsync();

            return Ok(TicketResponses);
        }
        [HttpGet("Get all Statues")]
        public async Task<IActionResult> GetStatusList()
        {
            var TicketResponses = await _TicketService.GetStatusListAsync();

            return Ok(TicketResponses);
        }

        [HttpGet("Get all users of type Team Member")]
        public async Task<IActionResult> GetTeamMemberList()
        {
            var UserResponses = await _TicketService.GetTeamMemberListAsync();

            return Ok(UserResponses);
        }
        [HttpPost("Edit Ticket")]
        public async Task<IActionResult> EditTicketManager(int userId, int ticketId)
        {
            var ticket = await _TicketService.EditTicketManagerAsync(userId, ticketId);

            if (!ticket)
            {
                return BadRequest("Failed to assign ticket");
            }

            return Ok("Ticket assigned successfully");
        }
    }
}
