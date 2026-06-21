using helpdesk.Contracts;
using MassTransit;

namespace helpdesk
{
    public class TicketAssignedConsumer : IConsumer<TicketAssigned>
    {
        private readonly ILogger<TicketAssignedConsumer> _logger;

        public TicketAssignedConsumer(ILogger<TicketAssignedConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(MassTransit.ConsumeContext<TicketAssigned> context)
        {
            
            _logger.LogInformation($"[*] Ticket ID: {context.Message.TicketId}");
            _logger.LogInformation($"[*] Agent ID: {context.Message.AgentId}");
            _logger.LogInformation($"[*] Date: {context.Message.AssignedAtUtc}");
            return Task.CompletedTask;
        }
    }
}