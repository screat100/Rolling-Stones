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
        timeStr = time.ToString("00.00");
        timeStr = timeStr.Replace(".", ":");

        TimeUI.text = ((int)time / 60).ToString() +":"+ timeStr;
        Speed.text = System.Math.Truncate(GameObject.Find("Player").GetComponent<Rigidbody>().velocity.magnitude).ToString();
    }
}
