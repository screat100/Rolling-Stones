using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStat : MonoBehaviour
{
    public float MaxHP = 100f;

    [System.NonSerialized]
    public float HP;


    void Start()
    {
        HP = MaxHP;
    }


    void Update()
    {
        if(HP <= 0)
        {
            FindObjectOfType<ui_manager>().isStageOver = true;
        }    
    }

    public void DoorTakeDamage(float Damage)
    {
        HP = HP - Damage;
    }
}
