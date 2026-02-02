using UnityEngine;
using Oculus.Interaction;
using UnityEngine.UI;
using TMPro;
public class SinkActivation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // 

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private GameObject indicator;

    private OVRInput.Controller rController;
    private OVRInput.Controller lController;
    private bool HandsIn = false;
    private Transform trackingSpace;

    public bool HANDWASH_COMPLETED = false;
    private Timer timer;
    private BoxCollider boxCol;


    void Start()
    {
        trackingSpace = FindFirstObjectByType<OVRCameraRig>().trackingSpace;

        boxCol = GetComponent<BoxCollider>();
        timer = GetComponent<Timer>();
        indicator.SetActive(false);
        timer.StopTimer();
    }
    private bool areHandsIn()
    {
        Vector3 rPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        Vector3 lPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        
        Vector3 globalRPos = trackingSpace.TransformPoint(rPos);

        Vector3 globalLPos = trackingSpace.TransformPoint(lPos);

         //// Debug.Log($"GlobalRPos: {globalRPos}");
        // // Debug.Log($"GlobalLPos: {globalLPos}");
        
        // Debug.Log($"boxCol.bounds.Contains(GlobalRPos): {boxCol.bounds.Contains(globalLPos)}\n");
        // Debug.Log($"boxCol.bounds.Contains(GlobalLPos): {boxCol.bounds.Contains(globalRPos)}\n");

        return boxCol.bounds.Contains(globalRPos) && boxCol.bounds.Contains(globalLPos);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //// Debug.Log("TRIGGER ENTER");
        // // Debug.Log($"BoxCol: {boxCol}\n");
        string dbgString = areHandsIn() ? "Hands Are In" : "Hands arent in :(";
        if (HandsIn)
        {
            timer.StartTimer();
            particles.Play();
            timer.isCountingDown = true;
        }
        //// Debug.Log(dbgString);


    }

    void OnTimerCompleted()
    {
        // Debug.Log("OnTimerCompleted Called\n");

        textDisplay.SetText("TIMER COMPLETED HANDS WASHED!!!!!! ! ! !\n");
        HANDWASH_COMPLETED = true;
        BroadcastMessage("OnInteractionCompleted");
        indicator.SetActive(true);
    }

    void OnTimerInterrupt(double timeRemaining)
    {
        // Debug.Log("Timer Interrupt Called");
        if (timeRemaining > 0)
        {

            HANDWASH_COMPLETED = false;
            // Debug.Log($"Washing finished with {timeRemaining} left\n");
        }

    }

    void OnTimerReset()
    {
        indicator.SetActive(false);
    }

    void ResetHandwash()
    {
        // Debug.Log("ResetHandwashCalled");
        particles.Stop();
        HANDWASH_COMPLETED = false;
        //timer.Reset();
    }
    private void OnTriggerExit(Collider collision)
    {

        string dbgString = areHandsIn() ? "Hands Are In" : "Hands arent in :(";
        // Debug.Log(dbgString);
        //textDisplay.SetText($"Hands Exited");
        if (!areHandsIn())
        {
            timer.StopTimer();
            particles.Stop();
            //ResetHandwash();
        }


    }

    void Update()
    {

        string dbgString = areHandsIn() ? "Hands Are In" : "Hands arent in :(";
        if (!HANDWASH_COMPLETED)
        {
            if (areHandsIn() && !timer.TIMER_COMPLETE)
            {
                particles.Play();
                textDisplay.SetText($"Remaining Time: {timer.timeRemaining.ToString("0.00")}\n");
                timer.isCountingDown = true;

            }
            else
            if (!areHandsIn())
            {
                textDisplay.SetText($"Hand Washing Station");
                particles.Stop();
                if (timer.isCountingDown)
                {
                    timer.StopTimer();
                }

            }
            if (timer.TIMER_COMPLETE)
            {
                OnTimerCompleted();
            }

           // // Debug.Log(dbgString);
        }
    }
    
}
