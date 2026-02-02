using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class FreeObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject _object;

    [SerializeField] GetObjectsInsideTrigger getobjs;

    [SerializeField] Material TrueMaterial;

    [SerializeField] Material FalseMaterial;
    
    [SerializeField] private TextMeshProUGUI _signText;

    private bool _HangingOn = true;

    private RequiredObjectsChecker _reqObjCheck;
    void Start()
    {
        _reqObjCheck = GetComponent<RequiredObjectsChecker>();
        getobjs.GetComponent<Renderer>().material = FalseMaterial;
        _signText.SetText("Press X to unhang Old IV Bag");
    }



    public void OnButtonClick()
    {
        // Debug.Log("Button Clicked");
        foreach (GameObject col in getobjs.TriggerList)
        {
            // Debug.Log($"Looking At Item {col.name}");
            Rigidbody _colObjectRigidBod = col.GetComponent<Rigidbody>();
            if(_HangingOn){
                // Debug.Log("Unlocking");
                _colObjectRigidBod.constraints = RigidbodyConstraints.None;
                col.BroadcastMessage("OnUnhang");
            }
            else
            {
                // Debug.Log("Locking");
                _colObjectRigidBod.constraints = RigidbodyConstraints.FreezeAll;
                col.BroadcastMessage("OnHang");
            }
        }
        getobjs.GetComponent<Renderer>().material = _HangingOn ? FalseMaterial : TrueMaterial;
        _HangingOn = _HangingOn ? false : true;
        string txt = _HangingOn ? "Press X to Hang IV Bag": "Press X to Unhang IV Bag";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
