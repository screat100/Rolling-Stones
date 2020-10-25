using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 파일로 저장할 데이터들을 이 클래스 내에 저장합니다.
[System.Serializable]
public class Data
{
    public int star;
    public List<float> record;
    
}

public class Record : MonoBehaviour
{
    public Data OnplayData;

    public int stageNum; //스테이지의 수

    public void SaveRecord(int stage, float time)
    {
        Debug.Log("stage = " + stage + ", time = " + time);
        //OnplayData.record[stage].Add(time);
    }

    public int getStar()
    {
        return OnplayData.star;
    }

    // 입력받은 스테이지의 최고기록을 반환
    public float getStageScoreBoard(int stage)
    {
        return OnplayData.record[stage];
    }


    public void SaveData()
    {
        // file save
        File.WriteAllText(Application.dataPath + "/record.json", JsonUtility.ToJson(OnplayData));
    }

    public void LoadData()
    {
        // file Load
        string str = File.ReadAllText(Application.dataPath + "/record.json");
        OnplayData = JsonUtility.FromJson<Data>(str);
    }


    void Start()
    {
        try
        {
            LoadData();
        }
        catch(FileNotFoundException)
        {
            /* initializing data file */
            OnplayData = new Data();
            OnplayData.star = 0;
            OnplayData.record = new List<float>();


            SaveData();
        }
        
        DontDestroyOnLoad(transform.gameObject);
        

    }


}
