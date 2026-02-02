using UnityEngine;
using Oculus.Interaction;
using System;
using System.Reflection;
using UnityEngine.XR.Hands;
using Oculus.Interaction.HandGrab;


public class OnSyringeGrab : MonoBehaviour
{   
    [SerializeField] Grabbable grabbable;
    [SerializeField] float minDistance = 0f; // Fully extended
    [SerializeField] float maxDistance;// Fully pressed
    private Color ogColor;

    private Vector3 OGPos;
    private Renderer PlungeRenderer;
    private OVRInput.Controller activeController;
    private bool GrabState = false;
    private PointerEvent Myevt;
    private float LastTriggerVal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlungeRenderer = GetComponent<Renderer>();
        ogColor = PlungeRenderer.material.color;
        grabbable.WhenPointerEventRaised += OnGrab;
    }
    void OnGrab(PointerEvent evt)
    {
        switch (evt.Type)
        {
            case PointerEventType.Select:
                PlungeRenderer.material.color = Color.red;
                OGPos = transform.localPosition;
                GrabState = true;
                Myevt = evt;
                break;

            case PointerEventType.Unselect:
                PlungeRenderer.material.color = ogColor;
                GrabState = false;
                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (GrabState)
        {


            Type MyevtDataType = Myevt.Data.GetType();

            float TriggerVal;
            if (Myevt.Data is HandGrabInteractor handGrab)
            {
                activeController = handGrab.Hand.Handedness == Oculus.Interaction.Input.Handedness.Right
                    ? OVRInput.Controller.RTouch
                    : OVRInput.Controller.LTouch;


                // Debug.Log($"Grabbed with {handGrab.Hand.Handedness} hand");
            }

            if (activeController == OVRInput.Controller.RTouch)
            {
                TriggerVal = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
            }
            TriggerVal = activeController == OVRInput.Controller.RTouch ? OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) : OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
            // Debug.Log($"TriggerVal = {TriggerVal}");

            // Calculate distance to move based on trigger
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
            { // A (right) or X (left)
                // Debug.Log("Button One pressed");
                maxDistance -= .001f;
                print($"Max Distance now: {maxDistance}");
            }
            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
            { // B (right) or Y (left)
                // Debug.Log("Button Two pressed");
                maxDistance += .001f;
                print($"Max Distance now: {maxDistance}");
            }
            float distance = Mathf.Lerp(minDistance, maxDistance, TriggerVal);

            if (distance == maxDistance)
            {
                PlungeRenderer.material.color = Color.green;
            }
            else
            {
                PlungeRenderer.material.color = Color.red;
            }
            // Move in the direction relative to parent's local space
            transform.localPosition = OGPos + (Vector3.back.normalized * distance);

        }
    }
    void OnDestroy()
    {
        if (grabbable != null)
        {
            grabbable.WhenPointerEventRaised -= OnGrab;
        }
        

    }
}
