using UnityEngine;

using UnityEngine;
using Oculus.Interaction;

public class AutoHandColorChanger : MonoBehaviour
{
    [SerializeField] private Color leftHandColor = Color.blue;
    [SerializeField] private Color rightHandColor = Color.red;
    
    private MaterialPropertyBlock propertyBlock;

    void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
        FindAndColorHands();
    }

    void FindAndColorHands()
    {
        // Find all SkinnedMeshRenderers in the scene
        SkinnedMeshRenderer[] renderers = FindObjectsOfType<SkinnedMeshRenderer>();

        foreach (var renderer in renderers)
        {
            Debug.Log($"Renderer Name: {renderer.name}");
            // Check if this is a hand renderer (adjust the name check based on your setup)
            if (renderer.name.Contains("HandVisual") || renderer.name.Contains("SyntheticHand"))
            {
                Color colorToUse = renderer.name.Contains("Left") ? leftHandColor : rightHandColor;
                
                renderer.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_Color", colorToUse);
                renderer.SetPropertyBlock(propertyBlock);
                
                Debug.Log($"Colored {renderer.name} with {colorToUse}");
            }
        }
    }
}