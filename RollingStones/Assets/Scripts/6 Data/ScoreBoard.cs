using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text board1;

    public Text board2_1;

    public Text board3_1;

    public Text board4_1;

    void Start()
    {
        //Debug.Log(GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record1.Count);

        //if (GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record1.Count != 0)
        //{
        //    GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record1.Sort();
        //}

        //// 각 데이터들을 정렬
        //for (int i = 1; i <= 4; i++) 
        //{
        //    Debug.Log(GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[i].Count);

        //    if (GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[i].Count != 0)
        //    {
        //        GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[i].Sort();
        //    }
        //}

        //// 1스테이지 기록
        //if (GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[1].Count != 0) 
        //{
        //    board1_1.text = GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[1][0].ToString();
        //}

        //// 2스테이지 기록
        //if (GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[2].Count != 0)
        //{
        //    board2_1.text = GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[2][0].ToString();
        //}

        //// 3스테이지 기록
        //if (GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[3].Count != 0)
        //{
        //    board3_1.text = GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[3][0].ToString();
        //}

        //// 4스테이지 기록
        //if (GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[4].Count != 0)
        //{
        //    board4_1.text = GameObject.Find("RecordManager").GetComponent<Record>().OnplayData.record[4][0].ToString();
        //}
    }
}
