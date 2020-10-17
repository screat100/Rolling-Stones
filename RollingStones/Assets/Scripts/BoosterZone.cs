using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoosterZone : MonoBehaviour
{
    public float Power = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //볼의 속력을 급속하게 증가!
            Rigidbody ballRG = other.gameObject.GetComponent<Rigidbody>();
            ballRG.velocity = new Vector3(0,0,transform.forward.z) * Power;

            FindObjectOfType<SoundManager>().PlayBoosterSound();
        }
    }
}
