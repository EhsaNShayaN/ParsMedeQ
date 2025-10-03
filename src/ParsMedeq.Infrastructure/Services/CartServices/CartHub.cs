using Microsoft.AspNetCore.SignalR;

namespace ParsMedeQ.Infrastructure.Services.CartServices;

public class CartHub : Hub
{
    // این متدها می‌تونه در صورت نیاز برای اتصال خاص کاربر استفاده بشه
    public async Task JoinCartGroup(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"cart-{userId}");
    }

    public async Task LeaveCartGroup(string userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"cart-{userId}");
    }
}