using helpdesk.Interfaces;
using helpdesk.Models;
using Microsoft.EntityFrameworkCore;

namespace helpdesk
{
    public class TicketService : ITicketServiceDb
    {
        private AppDbContext dbContext;
        public TicketService(AppDbContext appDb)
        {
            dbContext = appDb;
        }

        public async Task WriteTicketToDbAsync(CreateTicketDto dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description
            };
            await dbContext.AddAsync(ticket);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateTicketInDbAsync(int id, UpdateTicketDto dto)
        {
            var ticket = await dbContext.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return false;
            }
            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
           var affectedRows = await dbContext.Tickets.Where(t => t.Id == id).ExecuteDeleteAsync();
           return affectedRows > 0;
        }
        

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await dbContext.Tickets.FindAsync(id);
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await dbContext.Tickets.AsAsyncEnumerable().ToListAsync();
        }

        public async Task<bool> AssignTicketAsync(int ticketId, int agentId)
        {
            var ticket = await dbContext.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            ticket.Status = TicketStatus.InProgress;
            ticket.AssignedAgentId = agentId;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeStatusAsync(int ticketId, TicketStatus ticketStatus) 
        {
            var ticket = await dbContext.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }
            ticket.Status = ticketStatus;
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
    
}
