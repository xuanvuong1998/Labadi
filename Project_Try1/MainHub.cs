using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Project_Try1.Models;

namespace Project_Try1 {
    public class MainHub : Hub {
        public void Hello() {
            Clients.All.hello();
        }

        private static string hostID;
        private static string PIN;
        private static List<Player> players = new List<Player>();
        private static List<string> waitingPlayers = new List<string>();
        
        public void InitQuiz(string quizPIN) {     
            
            hostID = Context.ConnectionId;
            PIN = quizPIN;
            Clients.Caller.getPIN(quizPIN);
        }
       
        private bool IsJoined(string username) {
            return (players.Count(x => x.PlayerID == Context.ConnectionId
            || x.Name == username ) > 0);                
        }

        public void Join(string iPIN, string username) {

            if (PIN == iPIN && !IsJoined(username)) {                
                Clients.Client(hostID).notify(username);

                players.Add(new Player { Name = username, PlayerID = Context.ConnectionId });

                //Clients.User(hostID).notify(username);

                Clients.Caller.welcome();
            }else if (PIN != iPIN){
                Clients.Caller.sendErr("INVALID PIN");
            } else {
                Clients.Caller.sendErr("Duplicated Player's Nickname! Please choose another nick name");
            }
                        
        }

        public void AcceptPlayers() {
            waitingPlayers.Add(Context.ConnectionId);
        }
        public void sendQuestionsToClient(string content, string ans1, string ans2, 
            string ans3, string ans4, string ans, string image, string time) {
            //Clients.All.receiveQuestion(q);
            foreach (var player in waitingPlayers) {
                Clients.Client(player).receiveQuestion(content, ans1, ans2, ans3, ans4, ans, image, time);
            }
        }
       
    }
}