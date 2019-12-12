using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public float LimitTime;

    public void ClickReplay()
    {
        timerr.LimitTime = 0;
        SceneManager.LoadScene("GameScene");
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
