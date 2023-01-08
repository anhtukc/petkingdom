using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;

namespace PetKingdomFN.Controllers
{
    public class SellBillHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            base.OnConnectedAsync();
            Console.WriteLine(Context.ConnectionId);
            return Task.CompletedTask;
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine(Context.ConnectionId);
            base.OnDisconnectedAsync(exception);
            return Task.CompletedTask;
        }
    }
}
