using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager Instance  = new GameManager();

    public int playerId;
    void Start()
    {
        playerId=-1;
        Screen.SetResolution(640,480,false);
        Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
