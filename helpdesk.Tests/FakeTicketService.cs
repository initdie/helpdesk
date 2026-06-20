using helpdesk.Interfaces;
using helpdesk.Models;

namespace helpdesk.Tests;

public class FakeTicketService : ITicketServiceDb
{
    // заготовлені відповіді — тест їх виставляє перед викликом
    public Ticket? TicketToReturn;
    public bool BoolToReturn;
    public List<Ticket> ListToReturn = new();

    public Task<Ticket?> GetTicketByIdAsync(int id) => Task.FromResult(TicketToReturn);
    public Task<bool> AssignTicketAsync(int ticketId, int agentId) => Task.FromResult(BoolToReturn);
    public Task<bool> ChangeStatusAsync(int ticketId, TicketStatus status) => Task.FromResult(BoolToReturn);
    public Task<bool> UpdateTicketInDbAsync(int id, UpdateTicketDto dto) => Task.FromResult(BoolToReturn);
    public Task<bool> DeleteTicketAsync(int id) => Task.FromResult(BoolToReturn);
    public Task WriteTicketToDbAsync(CreateTicketDto dto) => Task.CompletedTask;
    public Task<List<Ticket>> GetTicketsAsync(TicketStatus? status) => Task.FromResult(ListToReturn);

    public Task<List<Ticket>> GetAllTicketsAsync(TicketStatus? status) => Task.FromResult(ListToReturn);
}