using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class OtherPlayersManager: MonoBehaviour
{
    //여기로 통한 플레이어 접근.
    public static OtherPlayersManager Instance = new OtherPlayersManager();
    public Dictionary<int,OtherPlayerController> Players = new Dictionary<int, OtherPlayerController>();

}
