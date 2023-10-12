using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ServerCore;
using System.Net;
using Google.Protobuf.Protocol;
using Google.Protobuf;
using StartMyFirstServer.Game;

namespace Server
{
	public class ClientSession : PacketSession
	{
		public Player MyPlayer { get; set; }
		public int SessionId { get; set; }
		public void Send(IMessage packet)
		{
			
			string msgName = packet.Descriptor.Name.Replace("_", string.Empty);
			MsgId msgId = (MsgId)Enum.Parse(typeof(MsgId), msgName);

			ushort size = (ushort)packet.CalculateSize();
			byte[] sendBuffer = new byte[size + 4];
			Array.Copy(BitConverter.GetBytes((ushort)(size + 4)), 0, sendBuffer, 0, sizeof(ushort));
			Array.Copy(BitConverter.GetBytes((ushort)msgId), 0, sendBuffer, 2, sizeof(ushort));
			Array.Copy(packet.ToByteArray(), 0, sendBuffer, 4, size);
			/*lock (_lock)
			{
				_reserveQueue.Add(sendBuffer);
				_reservedSendByte += sendBuffer.Length;
			}*/

			Send(new ArraySegment<byte>(sendBuffer));
		}

		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

			// PROTO Test
			MyPlayer = PlayerManager.Instance.Add();
            {
				MyPlayer.Info.PosX = 0;
				MyPlayer.Info.PosY = 0;
				MyPlayer.Info.PosZ = 0;
				MyPlayer.Info.RotX = 0;
				MyPlayer.Info.RotY = 0;
				MyPlayer.Info.RotZ = 0;
				MyPlayer.Info.VelX = 0;
				MyPlayer.Info.VelY = 0;
				MyPlayer.Info.VelZ = 0;
				MyPlayer.Session = this;

			}
			GameRoom.Instance.EnterGame(MyPlayer);
			




		}

		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
			PacketManager.Instance.OnRecvPacket(this, buffer);
		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			GameRoom.Instance.LeaveGame(MyPlayer.Info.PlayerId);
			SessionManager.Instance.Remove(this);

			Console.WriteLine($"OnDisconnected : {endPoint}");
		}

		public override void OnSend(int numOfBytes)
		{
			//Console.WriteLine($"Transferred bytes: {numOfBytes}");
		}
	}
}
