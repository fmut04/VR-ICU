using UnityEngine;

public class TapeZoneVisibilityHandler : MonoBehaviour
{

    private OVRCameraRig ovrCameraRig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ovrCameraRig = FindObjectOfType<OVRCameraRig>(); // Find the OVRCameraRig in the scene

    }

    private bool isPlayerWithinDist()
    {

        Vector3 playerGlobalPos = ovrCameraRig.trackingSpace.TransformPoint(ovrCameraRig.trackingSpace.localPosition);
        float dist = Vector3.Distance(playerGlobalPos, GetComponent<Transform>().TransformPoint(GetComponent<Transform>().position));
        float distIdk = Vector3.Distance(ovrCameraRig.trackingSpace.localPosition, GetComponent<Transform>().position);

        return dist <= 1.3 || distIdk <= 1.3;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
