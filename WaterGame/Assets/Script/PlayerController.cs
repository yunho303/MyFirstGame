
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{


    public bool spaceCool;
    public float rotateSpeed;

    public Image jumpImage;
    public Text scoreText;


    float hAxis;
    float vAxis;
    int point;
    bool swimming;
    Rigidbody rb;
    Animator ani;
    public Camera mainCamera;
    public float mousex;
    public float mousey;
    // Start is called before the first frame update
    void Start()
    {
        point=0;
        
        spaceCool = true;
        swimming = false;
        rb = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        //mainCamera.transform.localPosition = new Vector3(0,3,-2);
        //mainCamera.transform.rotation = Quaternion.Euler(50f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        OutofWater();
        Move();
        CheckSwim();
    }
    void LateUpdate()
    {

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

    void OutofWater(){
        if(transform.position.y>0){
            rb.useGravity = true;
        }else{
            rb.useGravity = false;
        }
    }

    void Move(){
        //rotateValue = new Vector3(pitchAmount)
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        // if(hAxis==0&&vAxis==0){
        //     ani.SetBool("Swim",false);
            
        // }else{
        //     ani.SetBool("Swim",true);
        // }
        

        //mousex = Input.GetAxis("Mouse X");
        //mousey = Input.GetAxis("Mouse Y");
        //Vector3 dir = new Vector3(hAxis,vAxis,0).normalized;
        Vector3 rotateValue = new Vector3(-vAxis * rotateSpeed, 
                              hAxis * rotateSpeed, 
                              0);
                              
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateValue * Time.deltaTime));


        //transform.Rotate(new Vector3(0,hAxis,0) * rotateSpeed *Time.deltaTime, Space.World); 
        

        if(Input.GetKeyDown(KeyCode.Space)&& spaceCool){
            StartCoroutine(SpaceCooltime());
            rb.AddForce(10 * transform.forward,ForceMode.Impulse);
        }
    }

    IEnumerator SpaceCooltime(){
        spaceCool = false;
        Cooltime jumpscript = jumpImage.GetComponent<Cooltime>();
        jumpscript.UseSkill(2.0f);
        Debug.Log("JUMP");
        yield return new WaitForSeconds(2.0f);
        spaceCool = true;
    }

    void OnCollisionEnter(Collision other){
        Debug.Log("Col!!");
        if(other.gameObject.CompareTag("Map")){
            StopCoroutine(Stun());
            StartCoroutine(Stun());
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag =="Point"){
            point++;
            UIManaer scoreTextScript = scoreText.GetComponent<UIManaer>();
            scoreTextScript.UpdateUI(point);
            Destroy(other.gameObject);
            Debug.Log($"GetPoint +{point}");
        }
    }
    IEnumerator Stun(){
        //rb.freezeRotation = true;
        yield return new WaitForSeconds(1.0f);
        rb.angularVelocity = Vector3.zero;
        //rb.freezeRotation = false;
        rb.velocity = Vector3.zero;
    }
}
