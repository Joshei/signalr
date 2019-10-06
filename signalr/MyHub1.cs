using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;




namespace SignalRChat


{
    


    public class ChatHub : Hub
    {
        //not best way
        //https://www.codeproject.com/Questions/233650/How-to-define-Global-veriable-in-Csharp-net
        private static int whoseturn = 0;
        private static int integer = 0;
        //int myplayernumber = -1;

        //clients : found in 
        private clients A_client = new clients();

        private static readonly List<clients> ClientList = new List<clients>();





        public override Task OnConnected()
        {
            

            return base.OnConnected();
        }

        
        public void printturn()
        {
            string name = "";
            if (whoseturn == 0)
            {

                
                name = "Player Two";
            }
            //second player
            if (whoseturn == 1)
            {
                
                // name = "Player One";
            }

            if (whoseturn == 0)
            {
                whoseturn = 1;
            }
            else if (whoseturn == 1)
            {
                whoseturn = 0;
            }

           
            Clients.Client(ClientList[0].ConnectionId).printname(name);
            Clients.Client(ClientList[1].ConnectionId).printname(name);
           

        }

        public void register(string name)
        {
            

            A_client.ConnectionId = Context.ConnectionId;
            A_client.Name = name;
            ClientList.Add(A_client);

            if (integer == 0)
            {
                Clients.Client(ClientList[0].ConnectionId).printinitial();
                integer = integer + 1;
            }
            else if (integer == 1)
            {
                Clients.Client(ClientList[1].ConnectionId).printinitial();
            }


        }
        //passes in message is which click button 
        //1 or zero
        public void play(string name, string message)
        {
            
            if (name == ClientList[whoseturn].Name)
            {
                
                if (message == "1")
                {

                    
                    Clients.Client(ClientList[0].ConnectionId).pressbutton(message);
                    Clients.Client(ClientList[1].ConnectionId).pressbutton(message);
                }
                if (message == "2")
                {

                    Clients.Client(ClientList[0].ConnectionId).pressbutton(message);
                    Clients.Client(ClientList[1].ConnectionId).pressbutton(message);

                }
                printturn();
            }
        }
    }
}