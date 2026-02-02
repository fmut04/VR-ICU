using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeOnClick : MonoBehaviour
{

    [SerializeField] string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnButtonClick()
    {

        // Debug.Log($"{gameObject.name}BUTTON CLICKED!!!!");
        SceneManager.LoadScene(sceneName);
    }
}
