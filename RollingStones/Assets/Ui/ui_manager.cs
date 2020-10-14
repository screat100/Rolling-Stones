﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using JetBrains.Annotations;

public class ui_manager : MonoBehaviour
{
    float time; // 스테이지 플레이시간
    public bool isStageOver; // 스테이지가 끝났는지 여부 판별

    public Text StageNumber;
    public Text TimeUI;
    public Text Speed;

    public GameObject ResultPopup;


    void Start()
    {
        // 스테이지 넘버 관련 코드
        StageNumber.text = SceneManager.GetActiveScene().name;

        time =0f;
    }

    void Update()
    {

        // 시계 관련 코드
        if(!isStageOver)
            time += Time.deltaTime;

        string timeStr;
        float minute = time % 60;
        int hour = (int)time / 60;
        timeStr = minute.ToString("00.00");
        timeStr = timeStr.Replace(".", ":");
        TimeUI.text = hour.ToString() + ":" + timeStr;

        // 스피드 관련 코드
        float Vx = GameObject.Find("Player").GetComponent<Rigidbody>().velocity.x;
        float Vz = GameObject.Find("Player").GetComponent<Rigidbody>().velocity.z;
        Speed.text = System.Math.Truncate(Mathf.Sqrt(Vx * Vx + Vz * Vz)).ToString();

        // 스테이지 종료 시 결과 팝업을 띄움
        if(isStageOver)
        {
            string star;

            // 클리어 시간에 따라 다른 별점 부여
            if(time<=70)                star = "★★★★★";
            else if (time <= 90)        star = "★★★★☆";
            else if (time <= 110)       star = "★★★☆☆";
            else if (time <= 130)       star = "★★☆☆☆";
            else                        star = "★☆☆☆☆";

            ResultPopup.SetActive(true);
            ResultPopup.transform.Find("Time").GetComponent<Text>().text = TimeUI.text;
            ResultPopup.transform.Find("Star").GetComponent<Text>().text = star;
        }

    }

}
