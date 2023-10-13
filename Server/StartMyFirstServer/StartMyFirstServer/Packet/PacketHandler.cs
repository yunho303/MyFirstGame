using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using StartMyFirstServer.Game;

class PacketHandler
{
    public static void C_MoveHandler(PacketSession session, IMessage packet)
    {
        C_Move movePacket = packet as C_Move;
        //받은 위치정보를 다른플레이어에게 전송.
        S_Move broadmovePacket = packet as S_Move;

        Console.WriteLine("Come");
        /*foreach (KeyValuePair <int,Player> p in PlayerManager.Instance._players)
        {
            if (p.Key != movePacket.PlayerInfo.PlayerId)
            {
                p.Value.Session.Send(broadmovePacket);
            }
            Console.WriteLine($"GetMovePacket by {movePacket.PlayerInfo.PlayerId}");
        }*/
    }

    public static void C_PongHandler(PacketSession session, IMessage packet)
    {
        Console.WriteLine("Pong");
    }
}