using Microsoft.AspNetCore.SignalR;

namespace HCMQuizGame.Hubs
{
    public class QuizHub : Hub
    {
        public static int OnlinePlayers = 0;

        public async Task JoinGame()
        {
            OnlinePlayers++;

            await Clients.All.SendAsync(
                "UpdatePlayerCount",
                OnlinePlayers
            );
        }
        public async Task StartGame()
        {
            await Clients.All.SendAsync("GameStarted");
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            OnlinePlayers--;

            if (OnlinePlayers < 0)
            {
                OnlinePlayers = 0;
            }

            await Clients.All.SendAsync(
                "UpdatePlayerCount",
                OnlinePlayers
            );

            await base.OnDisconnectedAsync(exception);
        }
    }
}