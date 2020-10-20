using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propeller : MonoBehaviour
{
    //float Speed = 50.0f;
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
        //if (other.gameObject.name == "Player")
        //{
        //    Rigidbody ballRG = other.gameObject.GetComponent<Rigidbody>();
        //    ballRG.velocity += new Vector3(transform.forward.x * Speed, 0, transform.forward.z * Speed);
        //}
    }
}
