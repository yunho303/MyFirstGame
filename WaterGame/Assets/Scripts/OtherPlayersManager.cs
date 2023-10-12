using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class OtherPlayersManager
{
    //여기로 통한 플레이어 접근.
    public static OtherPlayersManager Instance = new OtherPlayersManager();
    public Dictionary<int,GameObject> Players;

    
}
