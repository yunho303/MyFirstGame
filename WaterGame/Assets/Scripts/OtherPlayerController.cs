using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OtherPlayerController : MonoBehaviour
{

    //다른 플레이어 어캐.. 회전, 일단 입력한 걸로 transform 설정
    bool swimming;
    int point;
    Rigidbody rb;
    Animator ani;
    
    // Start is called before the first frame update
    void Start()
    {
        point=0;
        swimming=false;
        rb = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        //mainCamera.transform.localPosition = new Vector3(0,3,-2);
        //mainCamera.transform.rotation = Quaternion.Euler(50f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckSwim();
    }
    public void UpdateMoving(float x,float y, float z,float rotX,float rotY,float rotZ, float vecX,float vecY, float vecZ)
    {
        transform.position = new Vector3(x,y,z);
        transform.rotation = Quaternion.Euler(new Vector3(rotX,rotY,rotZ));
        rb.velocity = new Vector3(vecX,vecY,vecZ);
    }
    
    void CheckSwim(){
        if(rb.velocity.magnitude>1.0f){
            if(!swimming){
                StartCoroutine(Swim());
            }
        }
    }

    IEnumerator Swim(){
        ani.SetTrigger("Swim");
        swimming=true;
        yield return new WaitForSeconds(0.7f);
        swimming=false;
    }



}
