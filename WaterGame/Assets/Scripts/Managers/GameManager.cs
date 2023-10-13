using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager Instance  = null;
    public NetworkManager NetworkManager;
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
    void Update()
    {
        
    }
}
