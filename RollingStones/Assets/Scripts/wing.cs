using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wing : MonoBehaviour
{
    public float wingSpeed=0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localRotation *= Quaternion.Euler(0, wingSpeed, 0);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "Player")
    //    {
    //        Rigidbody ballRG = other.gameObject.GetComponent<Rigidbody>();
    //        Rigidbody wingRG = gameObject.GetComponent<Rigidbody>();
    //        ballRG.velocity += new Vector3(gameObject.transform.localRotation.x, 0, gameObject.transform.localRotation.z);
    //        ballRG.velocity += wingRG.velocity;
    //    }
    //}
}
