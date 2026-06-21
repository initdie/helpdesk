using helpdesk.Models.Enums;

namespace helpdesk.Models.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public int? AssignedAgentId { get; set; }
        public Ticket() { }
    }
}
