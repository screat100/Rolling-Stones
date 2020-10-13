using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ui_manager : MonoBehaviour
{
    float time;

    public Text StageNumber;
    public Text TimeUI;
    public Text Speed;

    // Start is called before the first frame update
    void Start()
    {
        time=0f;
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;

        StageNumber.text = SceneManager.GetActiveScene().name;

        string timeStr;
        float minute = time % 60;
        int hour = (int)time / 60;
        timeStr = minute.ToString("00.00");
        timeStr = timeStr.Replace(".", ":");
        TimeUI.text = hour.ToString() +":"+ timeStr;

        // x,z축의 벡터 증가만 속도로 침
        float Vx = GameObject.Find("Player").GetComponent<Rigidbody>().velocity.x;
        float Vz = GameObject.Find("Player").GetComponent<Rigidbody>().velocity.z;
        Speed.text = System.Math.Truncate(Mathf.Sqrt(Vx * Vx + Vz *Vz)).ToString();
    }

}
