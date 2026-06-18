using helpdesk.Models;

namespace helpdesk.Interfaces
{
    public interface ITicketServiceDb
    {
        Task WriteTicketToDbAsync(CreateTicketDto dto);
        Task<bool> UpdateTicketInDbAsync(int id, UpdateTicketDto dto);
        Task<bool> DeleteTicketAsync(int id);
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<bool> AssignTicketAsync(int ticketId, int agentId);
    }
}
