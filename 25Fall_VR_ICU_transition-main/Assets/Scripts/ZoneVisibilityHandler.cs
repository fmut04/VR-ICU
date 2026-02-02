using UnityEngine;
using System.Collections.Generic;
public class ZoneVisibilityHandler : MonoBehaviour
{

    private OVRCameraRig ovrCameraRig;

    public List<GameObject> trackedObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ovrCameraRig = FindObjectOfType<OVRCameraRig>(); // Find the OVRCameraRig in the scene
        foreach (GameObject gObj in trackedObjects)
        {
            Renderer gObjRenderer = gObj.gameObject.GetComponent<Renderer>();
            gObjRenderer.enabled = false;
        }
    }

    public bool isPlayerWithinDist()
    {

        Vector3 playerGlobalPos = ovrCameraRig.trackingSpace.TransformPoint(ovrCameraRig.trackingSpace.localPosition);
        float dist = Vector3.Distance(playerGlobalPos, GetComponent<Transform>().TransformPoint(GetComponent<Transform>().position));
        float distIdk = Vector3.Distance(ovrCameraRig.trackingSpace.localPosition, GetComponent<Transform>().position);

        return dist <= 1.3 || distIdk <= 1.3;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(isPlayerWithinDist()){
            foreach (GameObject gObj in trackedObjects)
            {
                Renderer gObjRenderer =gObj.gameObject.GetComponent<Renderer>();
                gObjRenderer.enabled = true;
            }

        }

    }    // Update is called once per frame

    void OnTriggerExit(Collider other)
    {
        
        if(isPlayerWithinDist()){
            foreach (GameObject gObj in trackedObjects)
            {
                Renderer gObjRenderer = gObj.gameObject.GetComponent<Renderer>();
                gObjRenderer.enabled = false;
            }

        }
    }
    void Update()
    {
        
    }
}
