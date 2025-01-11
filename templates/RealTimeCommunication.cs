public class ChatHub : Hub {
    public async Task SendMessage(string user, string message) {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}

// Add in Program.cs
app.MapHub<ChatHub>("/chatHub");
