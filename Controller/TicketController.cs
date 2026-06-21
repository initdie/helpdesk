using System.Security.Claims;
using helpdesk.Interfaces;
using helpdesk.Models.DTO;
using helpdesk.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace helpdesk.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketServiceDb _dbService;
        public TicketController(ITicketServiceDb ticketServiceDb)
        {
            _dbService = ticketServiceDb;
        }
        // GET: api/<TicketController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketStatus? status)
        {
            var tickets = await _dbService.GetAllTicketsAsync(status);
            if (tickets == null)
                return NotFound();
            return Ok(tickets);
        }

            
        

        // GET api/<TicketController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ticket = await _dbService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTicketDto dto)
        {
            await _dbService.WriteTicketToDbAsync(dto);
            return Ok();
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTicketDto dto)
        {
            var result = await _dbService.UpdateTicketInDbAsync(id, dto);   
            return result ? NoContent() : NotFound();
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _dbService.DeleteTicketAsync(id);
            return result ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Agent")]
        [HttpPatch("{ticketId}/assign")]
        public async Task<IActionResult> AssignTicket([FromBody] int ticketId)
        {
            var agentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _dbService.AssignTicketAsync(ticketId, agentId);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("{ticketId}/status")]
        public async Task<IActionResult> ChangeStatus(int ticketId, [FromBody] ChangeStatusDto dto)
        {
            var result = await _dbService.ChangeStatusAsync(ticketId, dto.Status);
            return result ? NoContent() : NotFound();
        }
    }
}
