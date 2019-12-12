using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public void ClickReplay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ClickExit()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}