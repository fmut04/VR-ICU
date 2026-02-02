using UnityEngine;

public class Timer : MonoBehaviour {
    public float duration;
    public float timeRemaining;
    public bool isCountingDown;

    public bool TIMER_COMPLETE;
    void Start()
    {
        timeRemaining = duration;
        isCountingDown = false;
        
    }
    public void StartTimer()
    {
        // Debug.Log("StartTimer");
        isCountingDown = true;
    }

    public void StopTimer()
    {
        // Debug.Log("Timer Stopping");
        isCountingDown = false;
    }

    public void Reset()
    {
        // Debug.Log("Timer Reset!!\n");
        isCountingDown = false;
        timeRemaining = duration;
        BroadcastMessage("OnTimerReset");
        TIMER_COMPLETE = false;

    }
    void Update()
    {

        if (isCountingDown)
        {
          //  // Debug.Log($"Timer Counting down to {timeRemaining - Time.deltaTime}");
            timeRemaining -= Time.deltaTime;
        }
        if ((timeRemaining <= (0 + 3*Time.deltaTime) || timeRemaining <= (0 - 3*Time.deltaTime)) && !TIMER_COMPLETE)
        {
            // Debug.Log("TIMER DONE!!!!\n");
            BroadcastMessage("OnTimerCompleted");
            TIMER_COMPLETE = true;
            StopTimer();
        }

    }


}
