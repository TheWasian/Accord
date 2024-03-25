using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listener;
        static void Main(string[] args)
        {
            //Thisgrabs the IP of the user and and changes it to a string
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine("This is your IP address: " + ip.ToString());
                    Console.WriteLine("Tell this IP to the person you want to chat with!");
                    _users = new List<Client>();
                    //this starts the function that listens for TCP connections from other computer
                    //and hosts the server on the users IP
                    _listener = new TcpListener(IPAddress.Parse(ip.ToString()), 7891);
                    _listener.Start();
                }
            }

            // this is for backup fixes _users = new List<Client>();
            //_listener = new TcpListener(IPAddress.Parse("10.218.185.43"), 7891);
            //_listener.Start();

            while (true)
            {
                //Accpets new users into the server
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);

                // Broadcast the connection to everyone on the server
                BroadcastConnection();
            }
        }

        static void BroadcastConnection()
        {
            foreach (var user in _users)
            {
                foreach (var usr in _users)
                {
                    //Translates other people user names into strings then displays them as connecting on the server
                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteMessage(usr.Username);
                    broadcastPacket.WriteMessage(usr.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
        }

        public static void BroadcastMessage(string message)
        {
            foreach (var user in _users)
            {
                //This displays the users message onto the server console
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(5);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }

        public static void BroadcastDisconnect(string uid)
        {
            //When a user exits Accord this will activate causing them to dissconnect from the server
            var disconnectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault();
            _users.Remove(disconnectedUser);

            foreach (var user in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);
                broadcastPacket.WriteMessage(uid);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }
            //When a user discornects it will broadcast this message in the server console this uses the user's username rather than their UID
            BroadcastMessage($"[{disconnectedUser.Username}] Disconnected!");
        }
    }
}
