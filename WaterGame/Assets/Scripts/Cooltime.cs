using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cooltime : MonoBehaviour
{
    Image image;
    // Start is called before the first frame update
    void Start(){
        image=GetComponent<Image>();
    }

    
    public void UseSkill(float cool){
        StartCoroutine("CoUseSkill",cool);
        //Debug.Log("StartCo JUMP");
    }
    IEnumerator CoUseSkill(float cool){
        //Debug.Log("?");
        image.fillAmount = 0;
        float plusnum = 1 / cool;

        while(image.fillAmount<1.0f){
            image.fillAmount+=Time.deltaTime*plusnum ;
            //Debug.Log(image.fillAmount);
            yield return null;
        }
    }
}
