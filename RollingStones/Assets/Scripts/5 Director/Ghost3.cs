using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Ghost3 : MonoBehaviour
{
    public static Ghost3 Instance;

    public GameObject Stage;
    public bool isGhostActivate;
    public bool isStageOver;

    //ArrayList ghostPos = new ArrayList();
    //ArrayList ghostRot = new ArrayList();

    //ArrayList ghostBackupPos = new ArrayList();
    //ArrayList ghostBackupRot = new ArrayList();

    public Vector3[] ghostBackupPos3 = new Vector3[9000];
    public Vector3[] ghostBackupRot3 = new Vector3[9000];

    public Vector3[] ghostPos3 = new Vector3[9000];
    public Vector3[] ghostRot3 = new Vector3[9000];

    int move;//배열
    float time;//플레이 시간

    public float record_time3;//전플레이어시간, 최단시간
    public float bestrecord_time3;

    void Start()
    {
        isGhostActivate = false;
        time = 0f;
        move = 0;
        bestrecord_time3 = 300.0f;
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
    public void CheckGhost3()
    {
        record_time3 = time;

        if (bestrecord_time3 > record_time3)
        {
            bestrecord_time3 = record_time3;
            for (int i = 0; i <= move; i++)
            {
                ghostPos3[i] = ghostBackupPos3[i];
                ghostRot3[i] = ghostBackupRot3[i];
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
            record_time3 = 0;
        }

        //InputGhost(Stage, bestrecord_time);
    }

    void FixedUpdate()
    {

        Debug.Log(ghostBackupPos3[move]);
        Debug.Log("/");
        Debug.Log(ghostBackupRot3[move]);
        move++;

        //기록
        ghostBackupPos3[move] = GameObject.Find("Player").transform.position;
        ghostBackupRot3[move] = GameObject.Find("Player").transform.forward;

        //고스트가 활성화되어있다면
        if (!FindObjectOfType<ui_manager>().isStageOver)
        {
            if (isGhostActivate)
            {
                //if (Stage.name == "Stage1")
                {
                    gameObject.transform.position = (Vector3)ghostPos3[move];
                    gameObject.transform.forward = (Vector3)ghostRot3[move];
                    //gameObject.transform.rotation = (Quaternion)ghostRot[time];
                }
            }
        }
    }
}