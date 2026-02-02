using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using UnityEngine.Rendering.Universal;
using System;

public class IVConnectorHandler : MonoBehaviour
{


    [SerializeField] Grabbable grabbable;

    [SerializeField] Renderer renda;
    private bool _GrabState;

    private bool _Connected;


    private Rigidbody _rigidBod;

    private PointerEvent _evt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _GrabState = false;
        _rigidBod = GetComponent<Rigidbody>();
        grabbable.WhenPointerEventRaised += OnGrab;
    }


    void OnGrab(PointerEvent evt)
    {
        switch (evt.Type)
        {
            case PointerEventType.Select:
                // Debug.Log("IVConnectorHandler Grabbed");
                _GrabState = true;
                _evt = evt;
                break;

            case PointerEventType.Unselect:
                // Debug.Log("IVConnectorHandler Not Grabbed");
                _GrabState = false;
                renda.material.color = Color.white;
                break;
        }

    }

    void OnCollisionExit(Collision collision)
    {
        renda.material.color = Color.white;
    }
    
    void OnTriggerStay(Collider collision)
    {
       // Debug.Log("IV CONNECT HANDLER: Trigger Stay!!");
        if (_GrabState)
        {
           // Debug.Log("_GrabState = True!");
            renda.material.color = Color.red;
           // Debug.Log($"Collision Tag: {collision.gameObject.tag}");
            if (collision.gameObject.tag == "IVConnectionZone")
            {

                renda.material.color = Color.green;
                if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
                { // A (right) or X (left)
                  //    // Debug.Log("Button pressed");
                    FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                    _rigidBod.isKinematic = true;
                    joint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
                    collision.gameObject.BroadcastMessage("OnIVConnect");
                }
                
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
