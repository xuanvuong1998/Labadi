﻿using System;
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
        }
       

        private bool IsJoined() {
            return (players.Count(x => x.PlayerID == Context.ConnectionId) > 0);
        }

        public void Join(string iPIN, string username) {

            if (PIN == iPIN && !IsJoined()) {                
                Clients.Client(hostID).notify(username);

                players.Add(new Player { Name = username, PlayerID = Context.ConnectionId });

                //Clients.User(hostID).notify(username);
            }
                        
        }

        public void AcceptPlayers() {
            waitingPlayers.Add(Context.ConnectionId);
        }
        public void sendQuestionsToClient(string q) {
            //Clients.All.receiveQuestion(q);
            foreach (var player in waitingPlayers) {
                Clients.Client(player).receiveQuestion(q);
            }
        }


    }
}