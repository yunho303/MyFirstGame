using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance = new ItemManager();
    public Dictionary<int,GameObject> Items = new Dictionary<int, GameObject>();
    
    
}
