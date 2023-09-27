using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float rotateSpeed;

    float hAxis;
    float vAxis;
    Rigidbody rb;
    public float mousex;
    public float mousey;
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Move();
    }

    

    void Move(){
        //rotateValue = new Vector3(pitchAmount)
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        //mousex = Input.GetAxis("Mouse X");
        //mousey = Input.GetAxis("Mouse Y");
        //Vector3 dir = new Vector3(hAxis,vAxis,0).normalized;
        Vector3 rotateValue = new Vector3(vAxis * rotateSpeed, 
                              hAxis * rotateSpeed, 
                              0);
                              
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateValue * Time.deltaTime));


        //transform.Rotate(new Vector3(0,hAxis,0) * rotateSpeed *Time.deltaTime, Space.World); 
        

        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(10 * transform.forward,ForceMode.Impulse);
        }
    }
}
