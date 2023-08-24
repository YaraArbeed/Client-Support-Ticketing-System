using Microsoft.AspNetCore.Mvc;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    [Route("api/[controller]")]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITicketService _TicketService;

        public TeamMemberController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        /// <summary>
        /// Retrieve all tickets assigned to a specific team member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportTeamTicketList(int id)
        {
            var TicketResponses = await _TicketService.GetSupportTeamTicketListAsync(id);

            return Ok(TicketResponses);
        }

        /// <summary>
        /// Update the details of a specific ticket,Only the assigned team member can perform this action
        /// </summary>
        /// <param name="id"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTicketTeamMember(int id, [FromBody] TicketEditTeamMemberParam param)
        {
            var ticket = await _TicketService.EditTicketTeamMemberAsync(id, param);
            return Ok(ticket);
        }

    }
}
