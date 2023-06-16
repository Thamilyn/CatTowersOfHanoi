using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timerValue;
    private bool isTimerRunning;

    public void StartTimer()
    {
        isTimerRunning = true;
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        CancelInvoke("UpdateTimer");
    }

    private void Start()
    {
        // Start the timer
        //InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    private void UpdateTimer()
    {
        if (!isTimerRunning)
            return;

        // Increment the timer value
        timerValue++;

        // Update the timer text
        int minutes = Mathf.FloorToInt(timerValue / 60f);
        int seconds = Mathf.FloorToInt(timerValue % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
