using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Google.Protobuf;

public class NetworkManager : MonoBehaviour
{
	ServerSession _session = new ServerSession();
    public static NetworkManager Network = new NetworkManager();

	public void Send(ArraySegment<byte> sendBuff)
	{
		_session.Send(sendBuff);
	}

    
    void Start(){
        Init();
        
        Debug.Log("NETSTART");
        
    }
    void Update(){
        Network.Update_NET();

    }
	public void Init()
	{
		// DNS (Domain Name System)
		string host = Dns.GetHostName();
		IPHostEntry ipHost = Dns.GetHostEntry(host);
		IPAddress ipAddr = ipHost.AddressList[4];
		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

		Connector connector = new Connector();

		connector.Connect(endPoint,
			() => { return _session; },
			1);
	}

	public void Update_NET()
	{
		List<PacketMessage> list = PacketQueue.Instance.PopAll();
		foreach (PacketMessage packet in list)
		{
			Action<PacketSession, IMessage> handler = PacketManager.Instance.GetPacketHandler(packet.Id);
			if (handler != null)
				handler.Invoke(_session, packet.Message);
		}	
	}

}
