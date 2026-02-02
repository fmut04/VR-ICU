using UnityEngine;

public class GloveHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private MeshCollider meshCol;
    [SerializeField] private Renderer leftHandRenderer;
    [SerializeField] private Renderer rightHandRenderer;
    [SerializeField] private Color handColor = Color.blue;

    [SerializeField] private GameObject indicator;
    private SkinnedMeshRenderer[] allRenderers;
    private MaterialPropertyBlock propertyBlock;

    public bool GLOVES_APPLIED = false;
    
    void Start()
    {
    }

    void OnEnable()
    {
        indicator.SetActive(false);
        allRenderers = FindObjectsOfType<SkinnedMeshRenderer>();
        
        // // Debug.Log($"Found {allRenderers.Length} SkinnedMeshRenderers:");
        // foreach (var renderer in allRenderers)
        // {
        //     // Debug.Log($"Renderer: {renderer.gameObject.name}, Path: {GetGameObjectPath(renderer.gameObject)}");
            
        //     // Debug the material and shader info
        //     if (renderer.sharedMaterial != null)
        //     {
        //         // Debug.Log($"Material: {renderer.sharedMaterial.name}, Shader: {renderer.sharedMaterial.shader.name}");
                
        //         // Check if material has color property
        //         if (renderer.sharedMaterial.HasProperty("_Color"))
        //         {
        //             // Debug.Log("Has _Color property");
        //         }
        //         if (renderer.sharedMaterial.HasProperty("_BaseColor"))
        //         {
        //             // Debug.Log("Has _BaseColor property");
        //         }
        //     }
        // }
    }

    string GetGameObjectPath(GameObject obj)
    {
        string path = obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = obj.name + "/" + path;
        }
        return path;
    }
    private void SetHandColor(Color newColor)
    {
        foreach (var renderer in allRenderers)
        {
            if (renderer == null || renderer.sharedMaterial == null) continue;
            
            // Debug.Log($"Attempting to color: {renderer.gameObject.name}");
            
            // Try multiple property names
            if (renderer.sharedMaterial.HasProperty("_Color"))
            {
                renderer.material.SetColor("_Color", newColor);
                // Debug.Log($"Set _Color on {renderer.gameObject.name}");
            }
            else if (renderer.sharedMaterial.HasProperty("_BaseColor"))
            {
                renderer.material.SetColor("_BaseColor", newColor);
                // Debug.Log($"Set _BaseColor on {renderer.gameObject.name}");
            }
            else
            {
                // Fallback: try direct color property
                try
                {
                    renderer.material.color = newColor;
                    // Debug.Log($"Set color directly on {renderer.gameObject.name}");
                }
                catch (System.Exception e)
                {
                    // Debug.LogWarning($"Could not set color on {renderer.gameObject.name}: {e.Message}");
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("COLLISION!!!!");
        // Debug.Log("With collider from object: " + other.gameObject.name);
        indicator.SetActive(true);
        BroadcastMessage("OnInteractionCompleted");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
