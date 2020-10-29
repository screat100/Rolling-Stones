using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Ghost2 : MonoBehaviour
{
    public GameObject Stage;
    //private ArrayList GhostPos = null;
    bool isGhostActivate;
    public bool isStageOver;

    //ArrayList ghostPos = new ArrayList();
    //ArrayList ghostRot = new ArrayList();

    //ArrayList ghostBackupPos = new ArrayList();
    //ArrayList ghostBackupRot = new ArrayList();

    public Vector3[] ghostBackupPos = new Vector3[9000];
    public Vector3[] ghostBackupRot = new Vector3[9000];

    public Vector3[] ghostPos = new Vector3[9000];
    public Vector3[] ghostRot = new Vector3[9000];

    int move;//배열
    float time;//플레이 시간

    public float record_time, bestrecord_time;//전플레이어시간, 최단시간

    void Start()
    {
        //isGhostActivate = false;
        time = 0f;
        move = 0;
        bestrecord_time = 300.0f;
        //InputGhost(StageNumber);
        // isGhostActivate를 체크하기 : 기록에서 확인한다.
    }

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        if (!isStageOver)
            time += Time.deltaTime;
    }

    // 스테이지가 종료되면 실행한다.
    public void CheckGhost()
    {
        isStageOver = true;

        record_time = time;

        if (bestrecord_time >= record_time)
        {
            bestrecord_time = record_time;
            for (move = 0; move <= 9000; move++)
            {
                ghostPos[move] = ghostBackupPos[move];
                ghostRot[move] = ghostBackupRot[move];
            }
            ghostBackupPos = null;
            ghostBackupRot = null;
            InputGhostPos(); InputGhostRot(); InputGhostTime();
            isGhostActivate = true;
        }

        else
        {
            isGhostActivate = false;
            ghostBackupPos = null;
            ghostBackupRot = null;
        }

        //InputGhost(Stage, bestrecord_time);
    }

    void FixedUpdate()
    {

        Debug.Log(ghostBackupPos[move]);
        Debug.Log("/");
        Debug.Log(ghostBackupRot[move]);
        move++;

        //기록
        ghostBackupPos[move] = GameObject.Find("Player").transform.position;
        ghostBackupRot[move] = GameObject.Find("Player").transform.forward;

        //고스트가 활성화되어있다면
        if (isGhostActivate)
        {
            gameObject.transform.position = (Vector3)ghostPos[move];
            gameObject.transform.forward = (Vector3)ghostRot[move];
            //gameObject.transform.rotation = (Quaternion)ghostRot[time];
        }
    }

    //public void SaveGhost()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath + "/Ghost");
    //    Debug.Log("File Location: " + Application.persistentDataPath + "/Ghost");
    //    // Write data to disk
    //    bf.Serialize(file, ghostPos);
    //    file.Close();
    //}
    //public void loadFromFile()
    //{
    //    //Check if Ghost file exists. If it does load it
    //    if (File.Exists(Application.persistentDataPath + "/Ghost"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/Ghost", FileMode.Open);
    //        GhostPos = (ArrayList)bf.Deserialize(file);
    //        file.Close();
    //    }
    //    else
    //    {
    //        Debug.Log("No Ghost Found");
    //    }
    //}

    public void InputGhostPos()
    {
        string m_strPath = "Assets/GhostInfoPos/";

        List<Vector3> GhostPos = new List<Vector3>();

        StreamReader Readfile = new StreamReader(m_strPath + "GhostInfoPos" + Stage.name + ".txt");

        for (move = 0; move <= 9000; move++)
        {
            GhostPos.Add(ghostPos[move]);
        }

        Readfile.Close();

        FileStream f = new FileStream(m_strPath + "GhostInfoPos" + Stage.name + ".txt", FileMode.Truncate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        for (move = 0; move <= 9000; move++)
        {
            writer.WriteLine(GhostPos[move]);
        }
        writer.Close();
    }

    public void InputGhostRot()
    {
        string m_strPath = "Assets/GhostInfoRot/";

        List<Vector3> GhostRot = new List<Vector3>();

        StreamReader Readfile = new StreamReader(m_strPath + "GhostInfoRot" + Stage.name + ".txt");

        for (move = 0; move <= 9000; move++)
        {
            GhostRot.Add(ghostRot[move]);
        }

        Readfile.Close();

        FileStream f = new FileStream(m_strPath + "GhostInfoRot" + Stage.name + ".txt", FileMode.Truncate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        for (move = 0; move <= 9000; move++)
        {
            writer.WriteLine(GhostRot[move]);
        }
        writer.Close();
    }
    public void InputGhostTime()
    {
        string m_strPath = "Assets/GhostInfoTime/";

        List<float> GhostTime = new List<float>();

        StreamReader Readfile = new StreamReader(m_strPath + "GhostInfoTime" + Stage.name + ".txt");
        GhostTime.Add(bestrecord_time);

        Readfile.Close();

        FileStream f = new FileStream(m_strPath + "GhostInfoTime" + Stage.name + ".txt", FileMode.Truncate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);

        writer.WriteLine(GhostTime);

        writer.Close();
    }
}