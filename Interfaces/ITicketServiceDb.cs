using helpdesk.Models.DTO;
using helpdesk.Models.Entities;
using helpdesk.Models.Enums;

namespace helpdesk.Interfaces
{
    public interface ITicketServiceDb
    {
        Task WriteTicketToDbAsync(CreateTicketDto dto);
        Task<bool> UpdateTicketInDbAsync(int id, UpdateTicketDto dto);
        Task<bool> DeleteTicketAsync(int id);
        Task<Ticket?> GetTicketByIdAsync(int id);
        Task<List<Ticket>> GetAllTicketsAsync(TicketStatus? status);
        Task<bool> AssignTicketAsync(int ticketId, int agentId);
        Task<bool> ChangeStatusAsync(int ticketId, TicketStatus ticketStatus);
    }
}
