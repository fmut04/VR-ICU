using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetObjectsInsideTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> TriggerList;

    public List<string> IgnoredTags;
    
    [SerializeField] private TextMeshProUGUI _SignTextObj;

    private RequiredObjectsChecker _reqObjCheck;
    private OVRCameraRig ovrCameraRig;
    
    void Start()
    {
        ovrCameraRig = FindObjectsByType<OVRCameraRig>(FindObjectsSortMode.None)[0]; // Find the OVRCameraRig in the scene

        _reqObjCheck = GetComponent<RequiredObjectsChecker>();
    }

//called when something enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Something Entered!");

        //if the object is not already in the list
        if(!TriggerList.Contains(other.gameObject) && !IgnoredTags.Contains(other.gameObject.tag) )
        {
            
            //add the object to the list
            // Debug.Log($"Adding Object {other.gameObject.name}");
            TriggerList.Add(other.gameObject);
        }
    }

    //called when something exits the trigger
    void OnTriggerExit(Collider other)
    {
        // Debug.Log("Something Exited");

        //if the object is in the list
        if(TriggerList.Contains(other.gameObject))
        {
            //remove it from the list
            TriggerList.Remove(other.gameObject);
        }
    }

    public bool isPlayerWithinDist()
    {

        Vector3 playerGlobalPos = ovrCameraRig.trackingSpace.TransformPoint(ovrCameraRig.trackingSpace.localPosition);
        Vector3 headsetPosition = ovrCameraRig.centerEyeAnchor.position;

        float dist = Vector3.Distance(playerGlobalPos, GetComponent<Transform>().TransformPoint(GetComponent<Transform>().position));
        float distIdk = Vector3.Distance(ovrCameraRig.centerEyeAnchor.position, GetComponent<Transform>().position);
        return dist <= 2.5 || distIdk <= 2.5;
    }

    void OnTriggerStay(Collider other)
    {
        if(isPlayerWithinDist()){
          if(_reqObjCheck.CheckObjectList(TriggerList)){
            
          _SignTextObj.SetText("Press X to Hang/Unhang IV Bag");
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
                  { // A (right) or X (left)
                     BroadcastMessage("OnButtonClick");
                  }
          }else{

            _SignTextObj.SetText("Required Objects not in IV Bag\n");
          }
        }else{
          _SignTextObj.SetText("Out of range for interaction");
        }
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
