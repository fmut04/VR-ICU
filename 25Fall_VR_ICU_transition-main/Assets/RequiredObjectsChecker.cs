using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class RequiredObjectsChecker : MonoBehaviour
{

    [SerializeField] List<string> requiredObjectTags;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (requiredObjectTags.Count ==0)
        {
            //Debug.Log("No Objects in List :(");
        }
        else
        {
           requiredObjectTags.Sort();

        }
    }



    public bool CheckObjectList(List<GameObject> other)
    {

        //Debug.Log("Got to CheckObjectList");
        if(requiredObjectTags.Count() == 0){ return true;}
        List<string> otherNames = other.ConvertAll<string>(GameObjectToName);

        //Debug.Log($"otherTags: {string.Join(",",otherNames)}");

        List<string> otherTags = other.ConvertAll<string>(GameObjectToTag);
        otherTags.Sort();
        //Debug.Log($"otherTags: {string.Join(",",otherTags)}");
        //Debug.Log($"requiredObjectTags: {string.Join(",",requiredObjectTags)}");
        if (requiredObjectTags.Count != otherTags.Count)
        {
            return false;
        }
        return otherTags.All(i=>requiredObjectTags.Contains(i));


    }

    public static string GameObjectToTag(GameObject obj)
    {
        return obj.gameObject.tag;

    }
    public static string GameObjectToName(GameObject obj)
    {
        return obj.gameObject.name;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
