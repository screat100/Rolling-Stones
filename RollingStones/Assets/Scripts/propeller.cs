using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propeller : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Rigidbody ballRG = other.gameObject.GetComponent<Rigidbody>();
            ballRG.velocity += new Vector3(transform.forward.x + gameObject.transform.position.x, 0, transform.forward.z + gameObject.transform.position.z);
        }
    }
}
