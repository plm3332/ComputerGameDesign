using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "GameScene";
    public float LimitTime;

    public void ClickStart()
    {
        timerr.LimitTime = 0;
        SceneManager.LoadScene(sceneName);
    }

    public void ClickExit()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}