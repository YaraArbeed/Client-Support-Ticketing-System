using Microsoft.AspNetCore.Mvc;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly ITicketService _TicketService;

        public ManagerController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        /// <summary>
        /// Retrieve all tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTicketList")]
        public async Task<IActionResult> GetTicketList()
        {
            var TicketResponses = await _TicketService.GetTicketListAsync();

            return Ok(TicketResponses);
        }

        /// <summary>
        /// Retrieve all Statues
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStatues")]
        public async Task<IActionResult> GetStatusList()
        {
            var TicketResponses = await _TicketService.GetStatusListAsync();

            return Ok(TicketResponses);
        }

        /// <summary>
        /// Retrieve all users of type 'Team Member'
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTeamMembers/{id}")]
        public async Task<IActionResult> GetTeamMemberList()
        {
            var UserResponses = await _TicketService.GetTeamMemberListAsync();

            return Ok(UserResponses);
        }
        /// <summary>
        /// Assign a specific ticket to a team member
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditTicketManager(int asigneeId, int ticketId, int statusId)
        {
            var ticket = await _TicketService.EditTicketManagerAsync(asigneeId, ticketId, statusId);

            if (!ticket)
            {
                return BadRequest("Failed to assign ticket");
            }

            return Ok("Ticket assigned successfully");
        }
    }
}
