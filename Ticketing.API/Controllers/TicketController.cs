using Microsoft.AspNetCore.Mvc;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Implementation;
using Ticketing.Services.Interface;

namespace Ticketing.API.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _TicketService;

        public TicketController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }
        /// <summary>
        /// Retrieve all tickets associated with a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketListById(int id)
        {
            var ticketResponses = await _TicketService.GetTicketListByIdAsync(id);
            return Ok(ticketResponses);
        }

        /// <summary>
        /// Add new ticket
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody] TicketParam param)
        {

            var ticket = await _TicketService.AddTicketAsync(param);
            return Ok(ticket);
        }

        /// <summary>
        /// "Retrieve details of a specific ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("View/{id}")]
        public async Task<IActionResult> ViewTicket(int id)
        {

            var ticket = await _TicketService.ViewTicketAsync(id);
            return Ok(ticket);
        }

        /// <summary>
        /// Update the details of a specific ticket
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTicket(int id, [FromBody] TicketEditParam Param)
        {
            var ticket = await _TicketService.EditTicketAsync(id,Param);
            return Ok(ticket);
        }

     
    }
}
