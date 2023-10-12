using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
public class PacketHandler
{
   public static void S_EnterGameHandler(PacketSession session, IMessage packet)
	{
		Debug.Log("환영합니다. 접속.");
		S_EnterGame enterGamePacket = packet as S_EnterGame;
		ServerSession serverSession = session as ServerSession;

		GameManager.Instance.playerId = enterGamePacket.Player.PlayerId;

	}
    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
	{
		
	}

	public static void S_SpawnHandler(PacketSession session, IMessage packet)
	{
		Debug.Log($"SpawnHandelr: {packet}");

		//여기서 만들어준다.
		S_Spawn spawnPacket = packet as S_Spawn;
		foreach(PlayerInfo p in spawnPacket.Players){
			GameObject obj = MonoBehaviour.Instantiate(Resources.Load("Prefabs/OtherPlayer")as GameObject);
			obj.name = $"Player {p.PlayerId}";
			OtherPlayerController opc = obj.GetComponent<OtherPlayerController>();
			opc.UpdateMoving(p.PosX,p.PosY, p.PosZ, p.RotX, p.RotY,p.RotZ,p.VelX,p.VelY,p.VelZ);
			OtherPlayersManager.Instance.Players.Add(p.PlayerId,obj);
		}
	}

	public static void S_DespawnHandler(PacketSession session, IMessage packet)
	{
		Debug.Log($"DeSpawnHandelr: {packet}");
	}

	public static void S_MoveHandler(PacketSession session, IMessage packet)
	{
		GameObject target=null;
		S_Move movePacket = packet as S_Move;
		if(!OtherPlayersManager.Instance.Players.TryGetValue(movePacket.PlayerInfo.PlayerId,out target ))
			return;

		
		//target =  OtherPlayersManager.Instance.Players[movePacket.PlayerInfo.PlayerId];
		OtherPlayerController opc = target.GetComponent<OtherPlayerController>();

		opc.UpdateMoving(movePacket.PlayerInfo.PosX,
		movePacket.PlayerInfo.PosY,
		movePacket.PlayerInfo.PosZ,
		movePacket.PlayerInfo.RotX,
		movePacket.PlayerInfo.RotY,
		movePacket.PlayerInfo.RotZ,
		movePacket.PlayerInfo.VelX,
		movePacket.PlayerInfo.VelY,
		movePacket.PlayerInfo.VelZ
		);
	}
}
