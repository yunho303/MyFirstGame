using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class PacketHandler
{
	public Dictionary<int,OtherPlayerController> Players;
   public static void S_EnterGameHandler(PacketSession session, IMessage packet)
	{
		Debug.Log("환영합니다. 접속.");
		S_EnterGame enterGamePacket = packet as S_EnterGame;
		ServerSession serverSession = session as ServerSession;

		GameManager.Instance.playerId = enterGamePacket.Player.PlayerId;
		GameManager.Instance.pc.plyaerIdText.GetComponent<UIManaer>().UpdateUI(GameManager.Instance.playerId);
	}
    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
	{
		//나 종료.
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
			//Debug.Log($"안됨+{p.PlayerId}");
			
			obj.GetComponentInChildren<UIManaer>().UpdateUI(p.PlayerId);
			OtherPlayersManager.Instance.Players.Add(p.PlayerId,opc);
			//Debug.Log($"안됨++{p.PlayerId}");
			if(opc==null){
				Debug.Log(null);
			}
			opc.UpdateMoving(p.PosX,p.PosY, p.PosZ, p.RotX, p.RotY,p.RotZ,p.VelX,p.VelY,p.VelZ);
			
		}
	}

	public static void S_DespawnHandler(PacketSession session, IMessage packet)
	{
		//타인 종료.
		S_Despawn despawnPacket = packet as S_Despawn;
		GameObject.Destroy(OtherPlayersManager.Instance.Players[despawnPacket.PlayerId].GameObject());
		
		OtherPlayersManager.Instance.Players.Remove(despawnPacket.PlayerId);

		Debug.Log($"DeSpawnHandelr: {packet}");
	}

	public static void S_MoveHandler(PacketSession session, IMessage packet)
	{
		

		OtherPlayerController target=null;
		S_Move movePacket = packet as S_Move;

		if(movePacket.PlayerInfo.PlayerId==GameManager.Instance.playerId)
			return;
		if(!OtherPlayersManager.Instance.Players.TryGetValue(movePacket.PlayerInfo.PlayerId,out target ))
			return;

		
		//target =  OtherPlayersManager.Instance.Players[movePacket.PlayerInfo.PlayerId];
		OtherPlayerController opc = target;

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

	public static void S_PingHandler(PacketSession session, IMessage packet)
	{
	}

	public static void S_MakeitemHandler(PacketSession session, IMessage packet)
	{
		//아이템 하나씩 생성되는거 생성.
		S_Makeitem makeItemPacket = packet as S_Makeitem;
		GameObject obj = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Object")as GameObject,
		new Vector3(makeItemPacket.Iteminfo.PosX,makeItemPacket.Iteminfo.PosY,makeItemPacket.Iteminfo.PosZ),
		Quaternion.identity);
		obj.name = $"Item {makeItemPacket.Iteminfo.ItemId}";
			
		ItemManager.Instance.Items.Add(makeItemPacket.Iteminfo.ItemId, obj);
	}
	public static void S_ScoreHandler(PacketSession session, IMessage packet)
	{
		//이러면 ScoreMAnager 필요없네 그냥 받아서 출력하면되네
		//일단 UI까지 연동해버리자.
		S_Score scorePacket = packet as S_Score;

		//아이템 삭제가 되나?..
		if(scorePacket.ItemId!=-1){
			GameObject.Destroy(ItemManager.Instance.Items[scorePacket.ItemId]);
			ItemManager.Instance.Items.Remove(scorePacket.ItemId);
		}
		

		List <ScoreInfo> SC = new List<ScoreInfo>();
		UIManaer scoreTextScript = GameManager.Instance.pc.scoreText.GetComponent<UIManaer>();
		foreach(ScoreInfo info in scorePacket.ScoreInfo){
			//탑 10 띄워주고
			
			SC.Add(info);
			if(info.PlayerId==GameManager.Instance.playerId){
				scoreTextScript.UpdateUI(info.Score);
			}

			
		}
		SC = SC.OrderBy(x=>x.Score).Reverse().ToList();
		GameManager.Instance.pc.score10Text.GetComponent<UIManaer>().UpdateUI(SC);
		

	}

	public static void S_GiveiteminfoHandler(PacketSession session, IMessage packet)
	{
		//아이탬 리스트 받아서 아이템 만들어서 출력.
		S_Giveiteminfo spawnPacket = packet as S_Giveiteminfo;
		foreach(ItemInfo item in spawnPacket.Iteminfos){
			GameObject obj = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Object")as GameObject,
			new Vector3(item.PosX,item.PosY,item.PosZ),
			Quaternion.identity);
			obj.name = $"Item {item.ItemId}";
			
			ItemManager.Instance.Items.Add(item.ItemId, obj);
			
			
		}

	}
}
