using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("SelectStage");
    }
}
