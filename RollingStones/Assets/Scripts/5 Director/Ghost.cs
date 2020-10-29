using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class location_Vector3
{
    private float x;
    private float y;
    private float z;

    public location_Vector3() { }
    public location_Vector3(Vector3 vec3)
    {
        this.x = vec3.x;
        this.y = vec3.y;
        this.z = vec3.z;
    }
    public static implicit operator location_Vector3(Vector3 vec3)
    {
        return new location_Vector3(vec3);
    }
    public static explicit operator Vector3(location_Vector3 wb_vec3)
    {
        return new Vector3(wb_vec3.x, wb_vec3.y, wb_vec3.z);
    }
}

[System.Serializable]
public class quaternion_Vector3
{
    private float w;
    private float x;
    private float y;
    private float z;

    public quaternion_Vector3() { }
    public quaternion_Vector3(Quaternion quat3)
    {
        this.x = quat3.x;
        this.y = quat3.y;
        this.z = quat3.z;
        this.w = quat3.w;
    }
    public static implicit operator quaternion_Vector3(Quaternion quat3)
    {
        return new quaternion_Vector3(quat3);
    }
    public static explicit operator Quaternion(quaternion_Vector3 wb_quat3)
    {
        return new Quaternion(wb_quat3.x, wb_quat3.y, wb_quat3.z, wb_quat3.w);
    }
}

[System.Serializable]
public class GhostShot
{
    public float timeMark = 0.0f;
    private location_Vector3 _posMark;
    public Vector3 posMark
    {
        get
        {
            if (_posMark == null)
                return Vector3.zero;
            else
                return (Vector3)_posMark;
        }
        set
        {
            _posMark = (location_Vector3)value;
        }
    }

    private quaternion_Vector3 _rotMark;
    public Quaternion rotMark
    {
        get
        {
            if (_rotMark == null)
                return Quaternion.identity;
            else
                return (Quaternion)_rotMark;
        }
        set
        {
            _rotMark = (quaternion_Vector3)value;
        }
    }

}



public class Ghost : MonoBehaviour
{
    private List<GhostShot> frameList;
    private List<GhostShot> lastReplayList = null;

    GameObject theGhost;

    private float replayTimescale = 1;
    private int replayIndex = 0;
    private float recordTime = 0.0f;
    private float replayTime = 0.0f;
    float bestrecord_time;
    public float nowPlayingtime; // 스테이지 플레이시간
    public bool isStageOver; // 스테이지 종료 여부
    bool isRecording = false, recordingFrame = false, isGhostActivate = false;

    public void loadFromFile()
    {
        if (File.Exists(Application.persistentDataPath + "/Ghost"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Ghost", FileMode.Open);
            lastReplayList = (List<GhostShot>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("No Ghost Found"); 
        }
    }

    void Start()
    {

        bestrecord_time = 300;
        isGhostActivate = false;
        StartRecording();
         
    }

    void Update()
    {
        if (!isStageOver)
            nowPlayingtime += Time.deltaTime;

    }
 
    public void SelectBestrecord()
    {
        if (bestrecord_time > nowPlayingtime)
        {
            isGhostActivate = true;
            bestrecord_time = nowPlayingtime;
            SaveGhostToFile();
        }
        else
        {
            isGhostActivate = false;
        }

    }

    public void CheckGhost()
    {
        if (lastReplayList != null && isGhostActivate)
        {
            MoveGhost();
        }
    }

    void FixedUpdate()
    {

        if (recordingFrame)
        {
            RecordFrame();
        }
        if (lastReplayList != null && isGhostActivate)
        {
            MoveGhost();
        }
    }

    private void RecordFrame()
    {
        recordTime += Time.smoothDeltaTime * 1000;
        GhostShot newFrame = new GhostShot()
        {
            timeMark = recordTime,
            posMark = this.transform.position,
            rotMark = this.transform.rotation
        };

        frameList.Add(newFrame);
    }

    public void StartRecording()
    {
        frameList = new List<GhostShot>();
        replayIndex = 0;
        recordTime = Time.time * 1000;
        recordingFrame = true;
        isGhostActivate = false;
    }

    public void StopRecordingGhost()
    {
        recordingFrame = false;
        lastReplayList = new List<GhostShot>(frameList);

        SelectBestrecord();
        //SaveGhostToFile();
    }

    public void playGhostRecording()
    {
        CreateGhost();
        replayIndex = 0;
        isGhostActivate = true;
    }

    public void StartRecordingGhost()
    {
        isRecording = true;
    }

    public void MoveGhost()
    {
        replayIndex++;

        if (replayIndex < lastReplayList.Count)
        {
            GhostShot frame = lastReplayList[replayIndex];
            DoLerp(lastReplayList[replayIndex - 1], frame);
            replayTime += Time.smoothDeltaTime * 1000 * replayTimescale;
        }
    }

    private void DoLerp(GhostShot a, GhostShot b)
    {
        if (GameObject.FindWithTag("Ghost") != null)
        {
            theGhost.transform.position = Vector3.Slerp(a.posMark, b.posMark, Mathf.Clamp(replayTime, a.timeMark, b.timeMark));
            theGhost.transform.rotation = Quaternion.Slerp(a.rotMark, b.rotMark, Mathf.Clamp(replayTime, a.timeMark, b.timeMark));
        }
    }

    public void SaveGhostToFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Ghost");
        Debug.Log("File Location: " + Application.persistentDataPath + "/Ghost");

        bf.Serialize(file, lastReplayList);
        file.Close();
    }

    public void CreateGhost()
    { 
        if (GameObject.FindWithTag("Ghost") == null)
        {
            theGhost = Instantiate(Resources.Load("GhostPrefab", typeof(GameObject))) as GameObject;
            theGhost.gameObject.tag = "Ghost";

            //Disable RigidBody
            //theGhost.GetComponent<Rigidbody>().isKinematic = true;

            MeshRenderer mr = theGhost.gameObject.GetComponent<MeshRenderer>();
            mr.material = Resources.Load("ghost_material", typeof(Material)) as Material;
        }
    }

}