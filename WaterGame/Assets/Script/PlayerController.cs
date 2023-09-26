using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float rollAmount;
    public float pitchAmount;
    public float yawAmount;
    Rigidbody rb;
    Vector3 rotateValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Move();
    }

    void Move(){
        //rotateValue = new Vector3(pitchAmount)
    }
}
