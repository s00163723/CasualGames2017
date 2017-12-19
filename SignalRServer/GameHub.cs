using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using CommonData;

namespace SignalRServer
{
    public class GameHub : Hub
    {
        #region gamehubvariables
        public static Queue<PlayerData> RegisteredPlayers = new Queue<PlayerData>(new PlayerData[]
        {
            new PlayerData {GamerTag = "Jimmy Boi",imageName = "",playerID = Guid.NewGuid().ToString() },
            new PlayerData {GamerTag = "Jimmy Boi",imageName = "",playerID = Guid.NewGuid().ToString() },
            new PlayerData {GamerTag = "Jimmy Boi",imageName = "",playerID = Guid.NewGuid().ToString() },
            new PlayerData {GamerTag = "Jimmy Boi",imageName = "",playerID = Guid.NewGuid().ToString() }
        });

        public static List<PlayerData> Players = new List<PlayerData>();

        public static Stack<string> characters = new Stack<string>(
            new string[] { "Player 4", "Player 3", "Player 2", "Player 1" });
        #endregion
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}