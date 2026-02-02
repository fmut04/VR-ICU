using UnityEngine;
using Oculus.Interaction;

public class GloveBoxHandler : MonoBehaviour
{

    [SerializeField] private SinkActivation sinkObj;
    [SerializeField] private GameObject glove;

    [SerializeField] private BoxCollider boxCol;

    [SerializeField] private GameObject glove_box_outer;

    [SerializeField] private GameObject glove_cube;

    [SerializeField] private GameObject glove_text;

    private float gloveDistanceThreshold;

    private OVRCameraRig ovrCameraRig;

    private bool ChildrenActive = false;

    void SetChildrenActiveState(bool state)
    {
        glove_box_outer.SetActive(state);
        glove_cube.SetActive(state);
        glove_text.SetActive(state);
        ChildrenActive = state;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ovrCameraRig = FindObjectOfType<OVRCameraRig>(); // Find the OVRCameraRig in the scene
        SetChildrenActiveState(true);
        glove.SetActive(false);
    }

    private bool isPlayerIn()
    {
        Vector3 playerGlobalPos = ovrCameraRig.trackingSpace.TransformPoint(ovrCameraRig.trackingSpace.localPosition);

        return boxCol.bounds.Contains(playerGlobalPos) || boxCol.bounds.Contains(ovrCameraRig.trackingSpace.position);
    }

    private bool isPlayerWithinDist()
    {

        Vector3 playerGlobalPos = ovrCameraRig.trackingSpace.TransformPoint(ovrCameraRig.trackingSpace.localPosition);
        float dist = Vector3.Distance(playerGlobalPos, GetComponent<Transform>().TransformPoint(GetComponent<Transform>().position));
       // float distIdk = Vector3.Distance(ovrCameraRig.trackingSpace.localPosition, GetComponent<Transform>().position);
        Debug.Log($"Distance: {dist}");
        return dist <= 7.5f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!ChildrenActive)
        {
           // // Debug.Log("HandWashCompleted!");
            SetChildrenActiveState(true);
        }

        if (isPlayerWithinDist() && ChildrenActive)
        {
         // Debug.Log("Setting Glove Active!!!");
            
         glove.SetActive(true);
        }
        else
        {
            // Debug.Log("Setting Glove False :(");
            glove.SetActive(false);
        }




    }
}
