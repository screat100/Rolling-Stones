using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class RankManagement : MonoBehaviour
{
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

        
        List<float> Rank=new List<float>(6);
        string path = Application.persistentDataPath;
        string filename="RankInfo"+Stage.name;

        //로컬 데이터 생성 
        //막판에 날 당황쓰 하게 만들었쓰~
        if(!Directory.Exists(path + "/Save"))
        {
            Directory.CreateDirectory(path + "/Save");
        }
        if(!File.Exists(path + "/Save/"+filename+".txt")){
            FileStream  f = new FileStream(path+"/Save/"+filename+".txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
            for (int i = 0; i < 5; i++){
                    writer.WriteLine(float.MaxValue);
            }
            writer.Close();
        }

        //스테이지별 RANKINFO를 불러와 출력
        //로컬 데이터에 저장된 정보 읽기
        StreamReader Readfile = new StreamReader(Application.persistentDataPath  + "/Save/RankInfo"+Stage.name+".txt");
        while(!Readfile.EndOfStream){
            string str;
            str = Readfile.ReadLine();
            if(str!=" ")
                Rank.Add(float.Parse(str));
         }

        //UI출력
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
