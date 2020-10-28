using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class RankManagement : MonoBehaviour
{
    string m_strPath = "Assets/RankInfo/";
    public GameObject Stage;
    void Start()
    {
        OutputRankText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OutputRankText(){

        //스테이지별 RANKINFO를 불러와 출력
        List<float> Rank=new List<float>(6);
        
        StreamReader Readfile = new StreamReader(m_strPath  + "RankInfo"+Stage.name+".txt");
        while(!Readfile.EndOfStream){
            string str;
            str = Readfile.ReadLine();
            if(str!=" ")
                Rank.Add(float.Parse(str));
        }
        Readfile.Close();
        
        
        for (int i=1;i<=5;i++)
        {
            string timeStr;
            float minute = Rank[i-1] % 60;
            int hour = (int)Rank[i-1] / 60;
            timeStr = minute.ToString("00.00");
            timeStr = timeStr.Replace(".", ":");
            if(Rank[i-1].ToString()==float.MaxValue.ToString()){
                transform.Find("rank"+i.ToString()).GetComponent<Text>().text="";
                
            }
            else{
                transform.Find("rank"+i.ToString()).GetComponent<Text>().text=i.ToString()+"등. "+(hour.ToString() + ":" + timeStr);
            }
        }
        
    }
}
