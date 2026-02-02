using UnityEngine;
using TMPro;
public class IVConnectionZone : MonoBehaviour
{

    [SerializeField] Material SuccessMaterial;
    [SerializeField] GameObject ConnectIVSign;
    [SerializeField] private TextMeshProUGUI _textDisplay;

    public bool IV_CONNECTED;

    private Renderer _Renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Renderer = GetComponent<Renderer>();
        IV_CONNECTED = false;
    }

    void OnIVConnect()
    {
        _Renderer.material = SuccessMaterial;
        _textDisplay.SetText("IV Connected!");
        BroadcastMessage("OnInteractionCompleted");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
