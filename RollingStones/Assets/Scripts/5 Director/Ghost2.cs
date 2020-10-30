using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Ghost2 : MonoBehaviour
{
    public static Ghost2 Instance;

    public GameObject Stage;
    public bool isGhostActivate;
    public bool isStageOver;

    //ArrayList ghostPos = new ArrayList();
    //ArrayList ghostRot = new ArrayList();

    //ArrayList ghostBackupPos = new ArrayList();
    //ArrayList ghostBackupRot = new ArrayList();

    public Vector3[] ghostBackupPos2 = new Vector3[9000];
    public Vector3[] ghostBackupRot2 = new Vector3[9000];

    public Vector3[] ghostPos2 = new Vector3[9000];
    public Vector3[] ghostRot2 = new Vector3[9000];

    int move;//배열
    float time;//플레이 시간

    public float record_time2;//전플레이어시간, 최단시간
    public float bestrecord_time2;

    void Start()
    {
        isGhostActivate = false;
        time = 0f;
        move = 0;
        bestrecord_time2 = 300.0f;
        isStageOver = false;
        // isGhostActivate를 체크하기 : 기록에서 확인한다.
    }

    void Awake()
    {
        Application.targetFrameRate = 30;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        if (!FindObjectOfType<ui_manager>().isStageOver)
            time += Time.deltaTime;
    }

    // 스테이지가 종료되면 실행한다.
    public void CheckGhost2()
    {
        record_time2 = time;

        if (bestrecord_time2 > record_time2)
        {
            bestrecord_time2 = record_time2;
            for (int i = 0; i <= move; i++)
            {
                ghostPos2[i] = ghostBackupPos2[i];
                ghostRot2[i] = ghostBackupRot2[i];
            }
            //ghostBackupPos = null;
            //ghostBackupRot = null;
            isGhostActivate = true;
            time = 0f;
            move = 0;

        }

        else
        {
            isGhostActivate = true;
            //ghostBackupPos = null;
            //ghostBackupRot = null;
            time = 0f;
            move = 0;
            record_time2 = 0;
        }

        //InputGhost(Stage, bestrecord_time);
    }

    void FixedUpdate()
    {

        Debug.Log(ghostBackupPos2[move]);
        Debug.Log("/");
        Debug.Log(ghostBackupRot2[move]);
        move++;

        //기록
        ghostBackupPos2[move] = GameObject.Find("Player").transform.position;
        ghostBackupRot2[move] = GameObject.Find("Player").transform.forward;

        //고스트가 활성화되어있다면
        if (!FindObjectOfType<ui_manager>().isStageOver)
        {
            if (isGhostActivate)
            {
                //if (Stage.name == "Stage1")
                {
                    gameObject.transform.position = (Vector3)ghostPos2[move];
                    gameObject.transform.forward = (Vector3)ghostRot2[move];
                    //gameObject.transform.rotation = (Quaternion)ghostRot[time];
                }
            }
        }
    }
}