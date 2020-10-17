using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float move = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody Ball = other.gameObject.GetComponent<Rigidbody>();

            Ball.velocity = gameObject.transform.forward * 5;
            Ball.velocity += new Vector3(0, transform.up.y, 0) * move;

        }
    }
}
