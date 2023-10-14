using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class PacketHandler
{
	public static void S_EnterGameHandler(PacketSession session, IMessage packet)
	{
		
		S_EnterGame enterGamePacket = packet as S_EnterGame;
		
	}
	public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
	{
		//나 종료.
	}

	public static void S_SpawnHandler(PacketSession session, IMessage packet)
	{
		

		//여기서 만들어준다.
		S_Spawn spawnPacket = packet as S_Spawn;
		
	}

	public static void S_DespawnHandler(PacketSession session, IMessage packet)
	{
		//타인 종료.
		S_Despawn despawnPacket = packet as S_Despawn;
		
	}

	public static void S_MoveHandler(PacketSession session, IMessage packet)
	{

	}

	public static void S_PingHandler(PacketSession session, IMessage packet)
	{
	}

	public static void S_MakeitemHandler(PacketSession session, IMessage packet)
	{
		//아이템 하나씩 생성되는거 생성.
		S_Makeitem makeItemPacket = packet as S_Makeitem;
		
	}
	public static void S_ScoreHandler(PacketSession session, IMessage packet)
	{
		//이러면 ScoreMAnager 필요없네 그냥 받아서 출력하면되네
		//일단 UI까지 연동해버리자.
		S_Score scorePacket = packet as S_Score;

		


	}

	public static void S_GiveiteminfoHandler(PacketSession session, IMessage packet)
	{
		//아이탬 리스트 받아서 아이템 만들어서 출력.
		S_Giveiteminfo spawnPacket = packet as S_Giveiteminfo;
		

	}
}
