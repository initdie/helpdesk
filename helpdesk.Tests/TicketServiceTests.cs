using helpdesk;
using helpdesk.Services;
using helpdesk.Models.DTO;
using helpdesk.Models.Entities;
using helpdesk.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace helpdesk.Tests;

public class TicketServiceTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetTicketByIdAsync_ReturnsTicket_WhenExists()
    {
        // Arrange — готуємо стан
        using var ctx = CreateContext();
        var ticket = new Ticket { Title = "Test", Description = "desc" };
        ctx.Tickets.Add(ticket);
        await ctx.SaveChangesAsync();          // тепер ticket.Id заповнений
        var service = new TicketService(ctx);

        // Act — викликаємо те, що тестуємо
        var result = await service.GetTicketByIdAsync(ticket.Id);

        // Assert — перевіряємо очікуване
        Assert.NotNull(result);
        Assert.Equal("Test", result!.Title);
    }

    [Fact]
    public async Task GetTicketByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        using var ctx = CreateContext();
        var service = new TicketService(ctx);

        // Act
        var result = await service.GetTicketByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AssignTicketAsync_ChangeFields_WhenAssignedTicket()
    {
        using var ctx = CreateContext();
        var service = new TicketService(ctx);

        await service.WriteTicketToDbAsync(new CreateTicketDto("Title_test","Desct_test"));
        await service.AssignTicketAsync(1,7);

        var result = await service.GetTicketByIdAsync(1);
        
        Assert.NotNull(result);
        Assert.Equal(TicketStatus.InProgress, result!.Status);
        Assert.Equal(7, result.AssignedAgentId);
    }

    [Fact]
    public async Task AssignTicketAsync_ChangeFields_WhenNoExist()
    {
        using var ctx = CreateContext();
        var service = new TicketService(ctx);

        var result = await service.AssignTicketAsync(777,7);

        Assert.False(result);
    }

    [Fact]
    public async Task ChangeStatusAsync_ChangeStatus_WhenExistTicket()
    {
        using var ctx = CreateContext();
        var service = new TicketService(ctx);

        var ticket = new Ticket {Title="T_2", Description = "D_2"};

        ctx.Tickets.Add(ticket);
        await ctx.SaveChangesAsync();
        var result = await service.ChangeStatusAsync(ticket.Id, TicketStatus.Done);

        Assert.True(result);
        var updated = await ctx.Tickets.FindAsync(ticket.Id);
        Assert.Equal(TicketStatus.Done, updated!.Status);
    }

    [Fact]
    public async Task ChangeStatusAsync_ChangeStatus_WhenNoExistTicket()
    {
        using var ctx = CreateContext();
        var service = new TicketService(ctx);

        var result = await service.ChangeStatusAsync(777,TicketStatus.Done);

        Assert.False(result);
    }

    [Fact]
    public async Task GetTicketsAsync_GetTicket_WhenFilterExist()
    {
        using var ctx = CreateContext();
        var service = new TicketService(ctx);

        ctx.Tickets.AddRange(
            new Ticket { Title="a", Description="d", Status=TicketStatus.Open },
            new Ticket { Title="b", Description="d", Status=TicketStatus.Open },
            new Ticket { Title="c", Description="d", Status=TicketStatus.Done });
        await ctx.SaveChangesAsync();

        var result = await service.GetAllTicketsAsync(TicketStatus.Open);
        
        Assert.All(result, t => Assert.Equal(TicketStatus.Open, t.Status));
        Assert.Equal(2,result.Count);
       
    }

    [Fact]
    public async Task GetTicketsAsync_GetTicket_WhenFilterNoExist()
    {
        using var ctx = CreateContext();
        var service = new TicketService(ctx);

        ctx.Tickets.AddRange(
            new Ticket { Title="a", Description="d", Status=TicketStatus.Open },
            new Ticket { Title="b", Description="d", Status=TicketStatus.Open },
            new Ticket { Title="c", Description="d", Status=TicketStatus.Done });
        await ctx.SaveChangesAsync();

        var result = await service.GetAllTicketsAsync(TicketStatus.InProgress);
        
        Assert.Empty(result);
    }
}
