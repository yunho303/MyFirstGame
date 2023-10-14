using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using Google.Protobuf.Protocol;
using UnityEngine;
using UnityEngine.UI;
public class UIManaer : MonoBehaviour
{
    public Text text;
    public TextMesh textMesh;
    void Start(){
        text = GetComponent<Text>();
        
        switch(type){
            case Type.Score:
                text.text = $"Score: 0";
                break;
            case Type.Score10:
                text.text = $"SCORE TOP 10\nhi";
                break;
        }
    }
    public enum Type {Score, Score10, Id};
    public Type type;
    // Start is called before the first frame update
    public void UpdateUI(int num){
        switch(type){
            case Type.Score:
                text.text = $"Score: {num}";
                break;
            case Type.Id:
                textMesh.text = $"Player {num}";
                break;
        }
    }

    public void UpdateUI(List<ScoreInfo> ScoreInfos){
        switch(type){
            
            case Type.Score10:
                //정렬되서온다.
                if(ScoreInfos.Count>10){
                    string s ="";
                    s+="TOP 10 SCORE\n\n";
                    for(int i=0;i<10;i++){
                        //10개 출력
                        s+=$"Player {ScoreInfos[i].PlayerId}: {ScoreInfos[i].Score}\n";
                    }
                    text.text = s;
                }else{
                    string s ="";
                    s+="TOP 10 SCORE\n\n";
                    for(int i=0;i<ScoreInfos.Count;i++){
                        //10개 출력
                        s+=$"Player {ScoreInfos[i].PlayerId}: {ScoreInfos[i].Score}\n";
                    }
                    text.text = s;
                }
                
                break;
        }
    }
}
