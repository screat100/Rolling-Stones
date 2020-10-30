using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToStage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void GoToStage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void GoToStage3()
    {
        SceneManager.LoadScene("Stage3");
    }
    public void GoToStage4()
    {
        SceneManager.LoadScene("Stage4");
    }
    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToSelectStage()
    {
        SceneManager.LoadScene("SelectStage");
        
        //Destroy(Ghost.Instance); Destroy(Ghost2.Instance2); Destroy(Ghost3.Instance3); Destroy(Ghost4.Instance4);
    }
    public void GoToTestStage()
    {
        SceneManager.LoadScene("TestStage");
    }


}
