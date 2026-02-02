using UnityEngine;
using UnityEngine.UI;

public class EnableCanvas : MonoBehaviour
{

    private Canvas _canv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     _canv = GetComponent<Canvas>();
    }
      

    void OnEnterState(){
      _canv.enabled = true;

    }

    void OnExitState(){
      _canv.enabled = false;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
