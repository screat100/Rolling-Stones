using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propeller : MonoBehaviour
{
    float Speed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnColliderEnter(Collider hit)
    {
        if (hit.gameObject.name == "Player")
        {
            Rigidbody ballRG = hit.gameObject.GetComponent<Rigidbody>();
            ballRG.velocity += new Vector3(transform.forward.x * Speed, 0, transform.forward.z * Speed);
        }
    }
}
