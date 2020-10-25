using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost : MonoBehaviour
{
    bool isGhostActivate;

    ArrayList ghostPos;
    ArrayList ghostRot;

    
    ArrayList ghostBackupPos;
    ArrayList ghostBackupRot;

    int time;

    void Start()
    {
        time = 0;

        // isGhostActivate를 체크하기 : 기록에서 확인한다.
    }

    // 스테이지가 종료되면 실행한다.
    public void CheckGhost()
    {

    }

    void FixedUpdate()
    {
        time++;

        //기록
        ghostBackupPos[time] = GameObject.Find("Player").transform.position;
        ghostBackupRot[time] = GameObject.Find("Player").transform.rotation;

        //고스트가 활성화되어있다면
        if (isGhostActivate)
        {
            gameObject.transform.position = (Vector3)ghostPos[time];
            gameObject.transform.rotation = (Quaternion)ghostRot[time];
        }


    }
}
