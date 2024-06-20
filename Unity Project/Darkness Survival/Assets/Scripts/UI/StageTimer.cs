using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float time;
    TimerUI timerUI;

    private void Start()
    {
        timerUI = FindObjectOfType<TimerUI>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        timerUI.UpdateTime(time);
    }
}
