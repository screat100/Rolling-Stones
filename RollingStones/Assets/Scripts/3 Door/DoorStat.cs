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
        if(HP <= 0 && !FindObjectOfType<ui_manager>().isStageOver)
        {
            FindObjectOfType<ui_manager>().stageClear();
            if(FindObjectOfType<ui_manager>().stage==1)
                FindObjectOfType<Ghost>().CheckGhost();
            else if (FindObjectOfType<ui_manager>().stage == 2)
                FindObjectOfType<Ghost2>().CheckGhost2();
            else if (FindObjectOfType<ui_manager>().stage == 3)
                FindObjectOfType<Ghost3>().CheckGhost3();
            else if (FindObjectOfType<ui_manager>().stage == 4)
                FindObjectOfType<Ghost4>().CheckGhost4();
        }    
    }

    public void DoorTakeDamage(float Damage)
    {
        HP = HP - Damage;
    }
}
