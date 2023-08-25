using Microsoft.AspNetCore.Mvc;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Implementation;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly ITicketService _TicketService;
        private readonly IUserService _userService;
        public ManagerController(ITicketService TicketService, IUserService userService)
        {
            _TicketService = TicketService;
            _userService = userService;
        }

        /// <summary>
        /// Retrieve all tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTickets")]
        public async Task<IActionResult> GetTicketList()
        {
            var TicketResponses = await _TicketService.GetTicketListAsync();

            return Ok(TicketResponses);
        }

        /// <summary>
        /// Retrieve all Statues
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllStatues")]
        public async Task<IActionResult> GetStatusList()
        {
            var TicketResponses = await _TicketService.GetStatusListAsync();

            return Ok(TicketResponses);
        }

        /// <summary>
        /// Retrieve all users of type 'Team Member'
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTeamMembers")]
        public async Task<IActionResult> GetTeamMemberList()
        {
            var UserResponses = await _TicketService.GetTeamMemberListAsync();

            return Ok(UserResponses);
        }


        /// <summary>
        /// Retrieve all users of type 'Client'
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetClientList()
        {
            var UserResponses = await _TicketService.GetClientListAsync();

            return Ok(UserResponses);
        }
        /// <summary>
        /// Assign a specific ticket to a team member and change statusId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpPost("EditTicket")]
        public async Task<IActionResult> EditTicketManager(int asigneeId, int ticketId, int statusId)
        {
            var ticket = await _TicketService.EditTicketManagerAsync(asigneeId, ticketId, statusId);

            if (!ticket)
            {
                return BadRequest("Failed to assign ticket");
            }

            return Ok("Ticket assigned successfully");
        }

        /// <summary>
        /// Activate a user by their ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("ActivateUser/{id}")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var UserActivation= await _userService.ActivateUserAsync(id);
            return Ok(UserActivation);
        }

        /// <summary>
        /// Deactivate a user by their ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("DeactivateUser/{id}")]
        public async Task<IActionResult> DeactiveUser(int id)
        {
            var UserDeactivation = await _userService.DeactiveUserAsync(id);
            return Ok(UserDeactivation);
        }
    }
}
