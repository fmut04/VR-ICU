using UnityEngine;

public class InteractionStatusHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject State;
    
    [SerializeField] string StepName;


    public void OnInteractionCompleted()
    {

      // Debug.Log($"On Interaction Completed Called with StepName: {StepName}");
      State.BroadcastMessage("OnStepCompleted",StepName);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
