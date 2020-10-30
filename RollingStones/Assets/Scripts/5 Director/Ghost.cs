using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class Ghost : MonoBehaviour
{
    /* 
    * 각 스테이지의 고스트 정보를 저장하는 클래스
    */
    public struct GhostInform
    {
        public bool hasGhost;           // 고스트의 정보가 저장되어있는가? (기본 false)
        public Vector3[] ghostPos;      // 고스트의 위치 정보를 저장
        public Quaternion[] ghostRot;   // 고스트의 회전 정보를 저장
        public float bestTime;          // 해당 스테이지의 최고기록(=최단시간) 저장
    }


    /* 
     * 업데이트가 있을 때마다 이 변수를 갱신해주세요.
     */
    int stageNum = 5;       // 스테이지 수
    /**/

    public GhostInform[] theGhost;      // 스테이지 수(+1)만큼 고스트 정보 생성

    // 다음 변수들은 ui_manager 스크립트에서 갱신합니다.
    public bool isPlaying;          // 현재 스테이지를 플레이중인가?
    public int stage;               // 현재 플레이중인 스테이지

    // 다음 변수들은 내부에서 갱신합니다.
    int frame;                      // 현재 플레이한 프레임
    Vector3[] ghostBackupPos;       // 현재 플레이한 포지션
    Quaternion[] ghostBackupRot;    // 현재 플레이한 로테이션

    void Start()
    {
        // Ghost Information Initializing
        theGhost = new GhostInform[stageNum];
        for(int i=0; i<stageNum; i++)
        {
            theGhost[i].hasGhost = false;
            theGhost[i].ghostPos = new Vector3[9001];
            theGhost[i].ghostRot = new Quaternion[9001];
            theGhost[i].bestTime = 300.0f;          // 기록은 최대 5분까지만 저장하도록 설정
            Debug.Log("besttime init val = " + theGhost[i].bestTime);
        }

        isPlaying = false;
        stage = -1;

        frame = 0;
        ghostBackupPos = new Vector3[9001];
        ghostBackupRot = new Quaternion[9001];

        Application.targetFrameRate = 30;   // frame update per second 세팅
        DontDestroyOnLoad(gameObject);      // 게임이 실행되는 중에는 고스트를 파괴하지 않는다 (정보를 저장하기 위함)

        for(int i=0; i<5; i++)
        {
            Debug.Log(theGhost[i].hasGhost);
        }
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        frame++;

        /*
         * 스테이지를 플레이 중일 때
         * (1) 고스트 녹화
         * (2) 고스트 재생 : 고스트가 존재한다면
         */
        if (isPlaying)
        {
            // 녹화
            if(frame <= 9000 && frame>=0)
                RecordGhost();

            // 재생
            if (theGhost[stage].hasGhost && frame>=0)
            {
                Debug.Log("frame = " + frame);
                Debug.Log(theGhost[stage].hasGhost);

                gameObject.transform.position = theGhost[stage].ghostPos[frame];
                gameObject.transform.rotation = theGhost[stage].ghostRot[frame];
            }
        }
    }

    /*
     * (1) 스테이지 시작 시 frame을 0으로 만든다.
     * (2) 고스트가 없으면 MeshRenderer를 끈다. (있으면 켠다)
     */
    public void RecordStart()
    {
        frame = 0;

        if(theGhost[stage].hasGhost)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

        else 
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    /*
     * 고스트를 기록(임시 백업)하는 함수
     * (1) 현재 플레이한 정보를 임시로 기록
     * (2) 저장되어있는 플레이타임과 비교
     * (3) 현재 플레이한 기록이 더 좋으면, 임시로 기록한 고스트 정보를 해당 스테이지에 저장... 아니라면 버림.
     */
    public void RecordGhost()
    {
        Debug.Log("Ghost Recording...");
        ghostBackupPos[frame] = GameObject.Find("Player").transform.position;
        ghostBackupRot[frame] = GameObject.Find("Player").transform.rotation;
    }

    /* 
     * 고스트를 저장하는 함수
     * ui_manager 에서 스테이지가 종료될 때 실행한다.
     */
    public void SaveGhost(float time)
    {
        Debug.Log("bestTime = " + theGhost[stage].bestTime);
        Debug.Log("now time = " + time);


        // 현재 플레이 타임이 더 좋은 기록(=적은 시간) 이라면
        if(time < theGhost[stage].bestTime)
        {
            Debug.Log("Ghost Saved!");


            for(int i=0; i<theGhost[stage].ghostPos.Length; i++)
            {
                theGhost[stage].ghostPos.Initialize();
                theGhost[stage].ghostRot.Initialize();
            }

            for(int i=0; i<ghostBackupPos.Length; i++)
            {
                theGhost[stage].ghostPos[i] = ghostBackupPos[i];
                theGhost[stage].ghostRot[i] = ghostBackupRot[i];
            }

            //theGhost[stage].ghostPos = ghostBackupPos;
            //theGhost[stage].ghostRot = ghostBackupRot;
        }

        theGhost[stage].hasGhost = true;

        // 백업 내용 비우기
        ghostBackupPos.Initialize();
        ghostBackupRot.Initialize();
    }
}