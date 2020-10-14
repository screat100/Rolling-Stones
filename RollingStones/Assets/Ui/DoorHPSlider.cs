using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class DoorHPSlider : MonoBehaviour
{

    Slider HPSlider;
    GameObject Door;
    float MaxHP;
    float CurrentHP;


    void Start()
    {
        Door = GameObject.Find("Door");
        CurrentHP = Door.GetComponent<DoorStat>().HP;
        MaxHP = Door.GetComponent<DoorStat>().MaxHP;

        HPSlider = GetComponent<Slider>();
        HPSlider.maxValue = MaxHP;
        HPSlider.value = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDoorHP();
    }

    private void UpdateDoorHP()
    {
        //Door의 체력을 계속해서 업데이트
        CurrentHP = Door.GetComponent<DoorStat>().HP;
        HPSlider.value = CurrentHP;

        //slider의 value가 0이 되도 체력이 조금남은것 처럼 표시되서 아예 꺼버림
        if (CurrentHP <= 0)
        {
            transform.Find("Fill Area").gameObject.SetActive(false);
        }
    }
}
