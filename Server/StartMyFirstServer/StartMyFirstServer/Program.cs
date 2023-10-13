using System;
using System.Net;
using System.Threading;
using ServerCore;
using StartMyFirstServer.Game;
/*using Server.Data;
using Server.DB;
using Server.Game;
using ServerCore;
using SharedDB;*/
//using static Google.Protobuf.Protocol.Person.Types;

namespace Server
{
    class Program
	{

		

		static Listener _listener = new Listener();

		public static int Port { get; } = 7777;
		public static string IpAddress { get; set; }

		static void FlushRoom()
		{
			JobTimer.Instance.Push(FlushRoom, 250);
		}
		static void Main(string[] args)
		{
			GameRoom gameRoom = new GameRoom();
			gameRoom.RoomId = 1;

			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			IPAddress ipAddr = ipHost.AddressList[4];
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
			IpAddress = ipAddr.ToString();

			_listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
			Console.WriteLine("Listening...");
			JobTimer.Instance.Push(FlushRoom);


			while (true)
			{
				JobTimer.Instance.Flush();
			}
		}
	}
}
