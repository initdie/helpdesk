using helpdesk.Models.Enums;

namespace helpdesk.Models.DTO
{
    public record CreateTicketDto(string Title, string Description);
    public record UpdateTicketDto(string Title, string Description);
    public record ChangeStatusDto(TicketStatus Status);
    public record ResponseTicketDto(string Title, string Description);
}

