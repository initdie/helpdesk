namespace helpdesk.Contracts
{
    public record TicketAssigned(int TicketId, int AgentId,  DateTime AssignedAtUtc);
}
