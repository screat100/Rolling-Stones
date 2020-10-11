using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStat : MonoBehaviour
{
    public float MaxHP = 100f;

    [System.NonSerialized]
    public float HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoorTakeDamage(float Damage)
    {
        HP = HP - Damage;
    }
}
