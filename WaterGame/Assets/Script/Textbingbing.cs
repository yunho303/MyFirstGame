using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Textbingbing : MonoBehaviour
{

    Rigidbody rb;

    bool isbingbing;
    public float bingbingpower;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isbingbing=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isbingbing){
            StartCoroutine("bingbing");
        }
        
        
    }

    IEnumerator bingbing(){
        isbingbing = true;
        rb.AddTorque(transform.up * bingbingpower, ForceMode.Impulse);
        yield return new WaitForSeconds(3.0f);

        isbingbing = false;
    }
}
