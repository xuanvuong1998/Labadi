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
        
        private static List<Player> players = new List<Player>();
        private static List<string> waitingPlayers = new List<string>();
        private static List<OpenQuiz> quizzes = new List<OpenQuiz>();
        private static Dictionary<string, int> correctsNum = new Dictionary<string, int>();
        
        public void InitQuiz(string quizPIN) {     
            
            string hostID = Context.ConnectionId;
            if (quizzes.Count(x => x.PIN == quizPIN) > 0) {
                Clients.Caller.sendErr();
            } else {
                quizzes.Add(new OpenQuiz { PIN = quizPIN, ConnectionID = Context.ConnectionId });
                Clients.Caller.getPIN(quizPIN);
            }            
        }
       
        private bool IsJoined(string PIN, string username) {
            return (players.Count(x => x.QuizPIN == PIN && (x.PlayerID == Context.User.Identity.Name
            || x.Name == username )) > 0);                
        }
        
        private bool isQuizOpening(string pin) {
            return quizzes.Count(x => x.PIN == pin) > 0;
        }

        private string getQuizConnectionID(string pin) {
            return quizzes.SingleOrDefault(x => x.PIN == pin).ConnectionID;
        }
        
        public void Join(string iPIN, string username) {

            bool opening = isQuizOpening(iPIN);

            if (opening && !IsJoined(iPIN, username)) {
                string hostID = getQuizConnectionID(iPIN);
                Clients.Client(hostID).notify(username);

                players.Add(new Player { Name = username, PlayerID = Context.ConnectionId 
                    , QuizPIN = iPIN});
                                

                Clients.Caller.welcome();
            }else if (opening == false) {
                Clients.Caller.sendErr("INVALID PIN");
            } else {
                Clients.Caller.sendErr("Duplicated Player's Nickname! Please choose another nick name");
            }                        
        }

        public void SendReport(bool res) {
            if (res == true) {
                correctsNum[Context.ConnectionId]++;
            }
        }

        public void SummaryResult(string quizPIN) {
            string summary = "";
            
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();

            for(int i = 0; i < waitingPlayers.Count; i++) {
                list.Add(new KeyValuePair<string, int>(players[i].Name, correctsNum[waitingPlayers[i]]));
            }

            list = list.OrderBy(x => x.Value).ToList();
            
            for(int i = list.Count - 1; i >= 0; i--) {
                summary += list[i].Key + " - " + list[i].Value + " Points";
                if (i > 0) summary += ";";
            }

            for (int i = 0; i < players.Count; i++) if (players[i].QuizPIN == quizPIN) {
                    Clients.Client(waitingPlayers[i]).sendSummary(summary);
                }
            //Clients.Clients(waitingPlayers).sendSummary(summary);
        }
        
        public void AcceptPlayers(){            
            waitingPlayers.Add(Context.ConnectionId);
            correctsNum[Context.ConnectionId] = 0;
        }
       

        public void sendQuestionsToClient(string content, string ans1, string ans2, 
            string ans3, string ans4, string ans, string image, string time, string quizPIN) {
            //Clients.All.receiveQuestion(q);                        
            for(int i = 0; i < players.Count; i++) if (players[i].QuizPIN == quizPIN) {                    
                    Clients.Client(waitingPlayers[i]).receiveQuestion(content, ans1, ans2,
                    ans3, ans4, ans, image, time);
                }            
        }


        public override Task OnDisconnected(bool stopCalled) {
            
            int foundIndex = -1;
            for(int i = 0; i < waitingPlayers.Count; i++) {
                if (waitingPlayers[i] == Context.ConnectionId) {
                    foundIndex = i;
                }
            }            

            if (foundIndex != -1) {
                players.RemoveAt(foundIndex);
                waitingPlayers.RemoveAt(foundIndex);
            }

            var item = quizzes.SingleOrDefault(x => x.ConnectionID == Context.ConnectionId);
            if (item != null) {
                quizzes.Remove(item);
            }
            
            return base.OnDisconnected(stopCalled);
        }
    }
}