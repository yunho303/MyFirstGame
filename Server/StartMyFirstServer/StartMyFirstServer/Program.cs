using System;
using System.Net;
using System.Threading;
using ServerCore;
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

		/*static void GameLogicTask()
		{
			while (true)
			{
				GameLogic.Instance.Update();
				Thread.Sleep(0);
			}
		}
		static void DbTask()
		{
			while (true)
			{
				DbTransaction.Instance.Flush();
				Thread.Sleep(0);
			}
		}*/


		/*static void NetworkTask()
		{
			while (true)
			{
				List<ClientSession> sessions = SessionManager.Instance.GetSessions();
				foreach (ClientSession session in sessions)
				{
					session.FlushSend();
				}
				Thread.Sleep(0);
			}
		}*/

		static Listener _listener = new Listener();

		/*static void StartServerInfoTask()
		{
			var t = new System.Timers.Timer();
			t.AutoReset = true;
			t.Elapsed += new System.Timers.ElapsedEventHandler((s, e) =>
			{
				//TODO
				using (SharedDbContext shared = new SharedDbContext())
				{
					ServerDb serverDb = shared.Servers.Where(s => s.Name == Name).FirstOrDefault();
					if (serverDb != null)
					{
						serverDb.IpAddress = IpAddress;
						serverDb.Port = Port;
						serverDb.BusyScore = SessionManager.Instance.GetBusyScore();
						shared.SaveChangesEx();
					}
					else
					{
						serverDb = new ServerDb()
						{
							Name = Program.Name,
							IpAddress = Program.IpAddress,
							Port = Program.Port,
							BusyScore = SessionManager.Instance.GetBusyScore()
						};
						shared.Servers.Add(serverDb);
						shared.SaveChangesEx();
					}
				}
			});
			t.Interval = 10 * 1000;
			t.Start();
		}*/
		public static string Name { get; } = "루나";
		public static int Port { get; } = 7777;
		public static string IpAddress { get; set; }

		static void Main(string[] args)
		{
			/*using (SharedDbContext shared = new SharedDbContext())
			{

			}
			ConfigManager.LoadConfig();
			DataManager.LoadData();




			GameLogic.Instance.Push(() =>
			{
				GameRoom room = GameLogic.Instance.Add(1);
			});*/


			// DNS (Domain Name System)
			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			IPAddress ipAddr = ipHost.AddressList[4];
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			IpAddress = ipAddr.ToString();

			//_listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
			Console.WriteLine("Listening...");

			//FlushRoom();



			//StartServerInfoTask();

			//DBTASK
			/*{
				Thread t = new Thread(DbTask);
				t.Name = "DB";
				t.Start();
			}*/

			//NetworkTask
			/*{
				Thread t = new Thread(NetworkTask);
				t.Name = "Network Send";
				t.Start();
			}*/

			//Gamelogic
			Thread.CurrentThread.Name = "Gamelogic";
			//GameLogicTask();
		}
	}
}
