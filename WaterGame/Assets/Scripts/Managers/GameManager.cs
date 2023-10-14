using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager Instance  = null;
    public NetworkManager NetworkManager;
    public PlayerController pc;
    public int playerId= -1;

    void Awake() {
        

        if(null==Instance){
            Instance=this;
        }else{
            Debug.Log("초기화안됨.");
        }
    }
    void Start()
    {
        //playerId=-1;
        Screen.SetResolution(640,480,false);
        Application.runInBackground = true;
    }

    // Update is called once per frame
    public static void GameExit()
    {
        C_LeaveGame leaveGamePacket = new C_LeaveGame();

        leaveGamePacket.PlayerId = Instance.playerId;

        GameManager.Instance.NetworkManager.Send(leaveGamePacket);
        Thread.Sleep(5);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
