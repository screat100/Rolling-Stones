using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wing : MonoBehaviour
{
    public float wingSpeed=0.2f;

    void FixedUpdate()
    {
        gameObject.transform.localRotation *= Quaternion.Euler(0, wingSpeed, 0);
    }

}
