using UnityEngine;
using Obi;

public class RopeSpringTension : MonoBehaviour
{
    public ObiRope rope;
    public Transform end1;
    public Transform end2;
    public float maxRopeLength = 2f;
    public float springStrength = 500f;
    public float springDamper = 50f;
    
    private SpringJoint joint;
    private Rigidbody rb1;
    
    void Start()
    {
        rb1 = end1.GetComponent<Rigidbody>();
        if (!rb1) rb1 = end1.gameObject.AddComponent<Rigidbody>();
    }
    
    void Update()
    {
        float currentDistance = Vector3.Distance(end1.position, end2.position);
        
        // Activate spring joint when rope is stretched
        if (currentDistance >= maxRopeLength * 0.95f)
        {
            if (joint == null)
            {
                joint = end1.gameObject.AddComponent<SpringJoint>();
                joint.connectedBody = end2.GetComponent<Rigidbody>();
                joint.spring = springStrength;
                joint.damper = springDamper;
                joint.maxDistance = maxRopeLength;
                joint.minDistance = 0;
            }
        }
        else if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
    }
}