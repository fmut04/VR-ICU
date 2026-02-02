using UnityEngine;
using TMPro;

public class TapeApplicationZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Material tapeAppliedMaterial;
    [SerializeField] GameObject ApplyTapeSign;

    [SerializeField] private TextMeshProUGUI _textDisplay;
    public bool TAPE_APPLIED;
    private Renderer _Renderer;
    void Start()
    {
        _Renderer = GetComponent<Renderer>();
        TAPE_APPLIED = false;
    }


    public void OnTapeApply()
    {
        _Renderer.material = tapeAppliedMaterial;
        _textDisplay.SetText("Tape Applied!");
        TAPE_APPLIED = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
