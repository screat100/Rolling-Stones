using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost : MonoBehaviour
{
    bool isGhostActivate;

    //ArrayList ghostPos = new ArrayList();
    //ArrayList ghostRot = new ArrayList();

    //ArrayList ghostBackupPos = new ArrayList();
    //ArrayList ghostBackupRot = new ArrayList();

    public Vector3[] ghostBackupPos = new Vector3[9000];
   // public Vector3[] ghostBackupRot = new Vector3[9000];
    public Vector3[] ghostPos = new Vector3[9000];
  //  public Vector3[] ghostRot = new Vector3[9000];

    int time;

    float record_time, bestrecord_time;

    public ui_manager _ui_manage;
    public static Ghost Instance;

    void Start()
    {

        time = 0;
        bestrecord_time = 300;
        isGhostActivate = false;

        // isGhostActivate를 체크하기 : 기록에서 확인한다.
    }

    void Awake()
    {
        Application.targetFrameRate = 30;
    }


    // 스테이지가 종료되면 실행한다.
    public void CheckGhost()
    {
       // ui_manager _ui_manage;
        record_time = _ui_manage.time;

        if (bestrecord_time >= record_time)
        {
            isGhostActivate = true;
            bestrecord_time = record_time;
            ghostPos[time] = ghostBackupPos[time];
            //ghostRot[time] = ghostBackupRot[time];
        }
        else
        {
            isGhostActivate = false;
            ghostBackupPos = null;
           // ghostBackupRot= null;
        }

    }

    void FixedUpdate()
    {
        Debug.Log(ghostBackupPos[time]);
        time++;

        //기록

        ghostBackupPos[time] = GameObject.Find("Player").transform.position;
        //ghostBackupRot[time] = GameObject.Find("Player").transform.rotation;
        //ghostBackupPos.Insert(time, GameObject.Find("Player").transform.position);
        //ghostBackupRot.Insert(time, GameObject.Find("Player").transform.rotation);

        //고스트가 활성화되어있다면
        if (isGhostActivate)
        {
            gameObject.transform.position = (Vector3)ghostPos[time];
           // gameObject.transform.rotation = (Quaternion)ghostRot[time];
        }


    }

  

}