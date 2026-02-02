using UnityEngine;

public class ButtonCallbackManager : MonoBehaviour
{  
    
    [SerializeField] GameObject _nurseDialogue;
    private string _callbackFunc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCallbackFunction(string callbackfunc){
      Debug.Log($"Got to SetCallbackFunction for button {gameObject.name}");
      _callbackFunc = callbackfunc;

    }

    public void OnButtonClick(){
      Debug.Log("Test");
      Debug.Log($"Got to OnButtonClick for button {gameObject.name}");
      _nurseDialogue.BroadcastMessage(_callbackFunc);
    }
}
