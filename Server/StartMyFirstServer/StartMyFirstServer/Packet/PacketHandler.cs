using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using ServerCore;
using StartMyFirstServer.Game;

class PacketHandler
{
    public static void C_MoveHandler(PacketSession session, IMessage packet)
    {
        C_Move movePacket = packet as C_Move;


        ClientSession clientSession = session as ClientSession;
        //받은 위치정보를 다른플레이어에게 전송.
        
        if (clientSession.MyPlayer == null)
            return;
        if (clientSession.MyPlayer.Room == null)
            return;

        S_Move broadmovePacket = new S_Move();

        //서버에서 좌표관리? x
        
        broadmovePacket.PlayerInfo = movePacket.PlayerInfo;
        clientSession.MyPlayer.Room.Broadcast(broadmovePacket);
        //Console.WriteLine("Come");
        
    }

    public static void C_PongHandler(PacketSession session, IMessage packet)
    {
        Console.WriteLine("Pong");
    }

    public static void C_GetitemHandler(PacketSession session, IMessage packet)
    {
        C_Getitem getItemPacket = packet as C_Getitem;
        Player player = null;
        GameRoom.Instance._players.TryGetValue(getItemPacket.PlayerId, out player);
        if (player == null)
            return;
        ItemInfo item = null;
        if (!GameRoom.Instance._items.TryGetValue(getItemPacket.ItemId, out item))
            return;


        GameRoom.Instance.DeleteItem(getItemPacket.ItemId);
        
        player.score++;




        //올린 스코어 Broadcast
        GameRoom.Instance.GiveAllScoreInfo(getItemPacket.ItemId);
        
    }
    public static void C_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        C_LeaveGame LGpacket = packet as C_LeaveGame;


        GameRoom.Instance.LeaveGame(LGpacket.PlayerId);

    }
}