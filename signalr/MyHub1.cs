using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;




namespace SignalRChat


{
    //public static class UserHandler
    //{
    //public static HashSet<string> ConnectedIds = new HashSet<string>();

    //}
    //public class clients
    //{
    //    public string ConnectionId { get; set; }
    //
    //}



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
            // var id = Context.ConnectionId;
            //CurrentConnections.Add(id);

            return base.OnConnected();
        }

        //return list of all active connections
        //public List<string> GetAllActiveConnections()
        //{
        //    
        //}

        public void printturn()
        {
            string name = "";
            if (whoseturn == 0)
            {

                //Clients.Client(ClientList[1].ConnectionId).printturn(name);
                name = "Player Two";// ClientList[1].Name;
            }
            //second player
            if (whoseturn == 1)
            {
                //Clients.Client(ClientList[0].ConnectionId).printturn(name);
                // name = "Player One";// ClientList[0].Name;
            }

            if (whoseturn == 0)
            {
                whoseturn = 1;
            }
            else if (whoseturn == 1)
            {
                whoseturn = 0;
            }

            //get rid of whoseturn
            Clients.Client(ClientList[0].ConnectionId).printname(name);
            Clients.Client(ClientList[1].ConnectionId).printname(name);
            //return (name);

        }









        public void register(string name)
        {
            int myplayernumber = -1;

            A_client.ConnectionId = Context.ConnectionId;
            A_client.Name = name;
            ClientList.Add(A_client);

            if (integer == 0)
            {
                myplayernumber = 1;
                integer = integer + 1;
            }
            else if (integer == 1)
            {
                Clients.Client(ClientList[1].ConnectionId).printinitial();
                myplayernumber = 2;
            }

            if (myplayernumber == 2)
            {
                //Clients.Client(ClientList[0].ConnectionId).pressbutton(message);
                Clients.Client(ClientList[1].ConnectionId).printinitial();
                //Clients.Client(ClientList[1].ConnectionId).printinitial(name);
            }




        }
        //passes in message is which click button 
        //1 or zero
        public void play(string name, string message)
        {
            //Debug.Write("play");
            //for (int i = 0; i <= 1; i++)
            //{
            //if the correct user is presing the click number button
            if (name == ClientList[whoseturn].Name)
            {
                //controls whos turn is it and sends name for printing


                //if (ClientList[i].ConnectionId == Context.ConnectionId)
                //{
                if (message == "1")
                {

                    //disabled buttons!
                    //Clients.AllExcept().pressbutton("1");
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

        //public void initial(string name)
        //{
        //    var a = 1;
        //if (name == ClientList[0].Name)
        //{



        //    //get rid of whoseturn
        //    Clients.Client(ClientList[0].ConnectionId).printname(name);
        //    Clients.Client(ClientList[1].ConnectionId).printname(name);
        //    //return (name);
        //}

        //}
    }

}