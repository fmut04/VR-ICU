using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using UnityEngine.Rendering.Universal;
using System;


public class TapeApplyHandler : MonoBehaviour
{

    private bool _GrabState;

    public GameObject m_DecalProjectorPrefab;
    [SerializeField] private float decalOffset = 0.01f;
    [SerializeField] private bool randomRotation = true;
    [SerializeField] Grabbable grabbable;
    [SerializeField] Renderer renda;

    [SerializeField] private LayerMask decalLayers = ~0;
    private PointerEvent _evt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _GrabState = false;
        grabbable.WhenPointerEventRaised += OnGrab;

    }

    void OnGrab(PointerEvent evt)
    {
        //// Debug.Log("PointerEvent");
        switch (evt.Type)
        {
            case PointerEventType.Select:
              //  // Debug.Log("Grabbed");
                _GrabState = true;
                _evt = evt;
                break;

            case PointerEventType.Unselect:
             //   // Debug.Log("Not Grabbed");
                _GrabState = false;
                renda.material.color = Color.white;
                break;
        }


    }
    void OnCollisionEnter(Collision collision)
    {
    

    }

    void OnCollisionExit(Collision collision)
    {
        renda.material.color = Color.white;
    }

    void OnCollisionStay(Collision collision)
    {
       // // Debug.Log("TAPE APPLY HANDLER: Trigger Stay!!");
        if (_GrabState)
        {
           // // Debug.Log("_GrabState = True!");
            renda.material.color = Color.red;
           // // Debug.Log($"Collision Tag: {collision.gameObject.tag}");
            if (collision.gameObject.tag == "TapeApplicationZone")
            {

                renda.material.color = Color.green;
                if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
                { // A (right) or X (left)
                //    // Debug.Log("Button pressed");

                    collision.gameObject.BroadcastMessage("OnTapeApply");
                }
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        // Debug.Log("Trigger Exit");        
        renda.material.color = Color.white;
    }
    
    void SpawnDecalAtImpact(Collision collision)
    {

        ContactPoint contact = collision.contacts[0];

        GameObject decal = Instantiate(
            m_DecalProjectorPrefab,
            contact.point + contact.normal * 0.01f,
            Quaternion.LookRotation(Vector3.up, -contact.normal)
        );
        
        decal.transform.SetParent(collision.transform);

        
        //// Debug.Log($"Decal spawned");
    }
}
