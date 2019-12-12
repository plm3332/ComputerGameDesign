using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public float LimitTime;
    public Text text_Timer;
    // Start is called before the first frame update
    void Start()
    {
        text_Timer.text = "Time: " + Mathf.Round(timerr.LimitTime);
        //timerr.LimitTime = 0;
    }

    void Update()
    {
        timerr.LimitTime = 0;
    }
}
