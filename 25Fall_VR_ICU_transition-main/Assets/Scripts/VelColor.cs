//using UnityEditor.EngineDiagnostics;
using UnityEngine;

public class VelColor : MonoBehaviour
{

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            // Debug.Log("RigidBody not found!\n");
            this.enabled = false;

        }
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        Vector3 vel = rb.linearVelocity;
        GetComponent<Renderer>().material.color = new Color(123, 123, vel.magnitude);

        
    }



    void Update()
    {
        
    }
}
