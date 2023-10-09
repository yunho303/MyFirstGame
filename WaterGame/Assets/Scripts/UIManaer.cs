using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManaer : MonoBehaviour
{
    public Text text;
    void Start(){
        text = GetComponent<Text>();
        text.text = $"Score: 0";
    }
    public enum Type {Score, ETC};
    public Type type;
    // Start is called before the first frame update
    public void UpdateUI(int num){
        switch(type){
            case Type.Score:
                text.text = $"Score: {num}";
                break;
        }
    }
}
