using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using JetBrains.Annotations;
using System.IO;

public class ui_manager : MonoBehaviour
{
    public int stage; //현재가 몇 스테이지인지
    public float time; // 스테이지 플레이시간
    public bool isStageOver; // 스테이지 종료 여부

    public Text StageNumber;
    public Text TimeUI;
    public Text Speed;

    public GameObject ResultPopup;
    Text damageFont;

    public int starTime5, starTime4, starTime3, starTime2;


    void Start()
    {
        // 스테이지 넘버 관련 코드
        StageNumber.text = SceneManager.GetActiveScene().name;

        ResultPopup.SetActive(false);

        // damage font
        damageFont = gameObject.transform.Find("DamageFont").GetComponent<Text>();
        damageFont.gameObject.SetActive(false);

        time =0f; // Playtime
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
        int BallSpeed = (int)(GameObject.Find("Player").GetComponent<Rigidbody>().velocity.magnitude);
        Speed.text = BallSpeed.ToString();

    }

    // 스테이지 종료 시
    public void stageClear()
    {
        isStageOver = true;

        /* 결과팝업 */
        string star;

        // 클리어 시간에 따라 다른 별점 부여
        if (time <= starTime5) star = "★★★★★";
        else if (time <= starTime4) star = "★★★★☆";
        else if (time <= starTime3) star = "★★★☆☆";
        else if (time <= starTime2) star = "★★☆☆☆";
        else star = "★☆☆☆☆";

        ResultPopup.SetActive(true);
        ResultPopup.transform.Find("Time").GetComponent<Text>().text = TimeUI.text;
        ResultPopup.transform.Find("Star").GetComponent<Text>().text = star;

        /* 결과데이터 저장 */

        Ghost.Instance.CheckGhost();
        InputRank(StageNumber,time);
    }

    public void DamageFontOn(float damage)
    {
        damageFont.gameObject.SetActive(true);
        damageFont.text = damage.ToString();

        Invoke("DamageFontOff", 2);
    }

    public void DamageFontOff()
    {
        damageFont.gameObject.SetActive(false);
    }

    //결과 시간 RankInfo 저장
    void InputRank(Text Stage,float mytime)
    {
        string m_strPath = "Assets/RankInfo/";

        List<float> Rank=new List<float>();

        StreamReader Readfile = new StreamReader(m_strPath  + "RankInfo"+Stage.text+".txt");
        //랭크 시간이 저장된 파일을 불러들어 읽고 내 스테이지 클리어 시간을 삽입후 정렬.
        while(!Readfile.EndOfStream){
            string str;
            str = Readfile.ReadLine();
            if(str!=" ")
                Rank.Add(float.Parse(str));
        }

        Rank.Add(mytime);
        Readfile.Close();

        Rank.Sort();

        FileStream  f = new FileStream( m_strPath  + "RankInfo"+Stage.text+".txt", FileMode.Truncate, FileAccess.Write);
        //정렬된 정렬된 벡터를 다시 파일에 다시 써주기!
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        for (int i = 0; i < 5; i++){
            if(Rank.Count<i+1){
                 writer.WriteLine(float.MaxValue);
            }
            else{
                 writer.WriteLine(Rank[i]);
            }
        }

        writer.Close();
    }
}
