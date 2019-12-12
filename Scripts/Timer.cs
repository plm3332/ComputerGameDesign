using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float LimitTime;
    public Text text_Timer;
    // Update is called once per frame

    void Update()
    {
        timerr.LimitTime += Time.deltaTime;
        text_Timer.text = "Time: " + Mathf.Round(timerr.LimitTime);
    }
}
