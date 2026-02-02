using UnityEngine;
using System.Collections.Generic;
public class HangingHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created\

    [SerializeField] List<GameObject> ControlledVisibilityObjects;
    [SerializeField] bool _newBag;
    public bool OBJECT_HANGING = false;
    void Start()
    {
        
    }

    void OnHang()
    {
        OBJECT_HANGING = true;
        foreach (GameObject gObj in ControlledVisibilityObjects)
        {
            gObj.SetActive(true);

        }
            
        if(_newBag){
          BroadcastMessage("OnInteractionCompleted");
        }
        
    }

    void OnUnhang()
    {
        OBJECT_HANGING = false;
        foreach (GameObject gObj in ControlledVisibilityObjects)
        {
            gObj.SetActive(false);

        }
    }
}
