using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

using UnityEngine.UIElements;
public class State : MonoBehaviour
{
    [Header("State Configuration")]
    [SerializeField] string stateName;
    [SerializeField] private int numberOfRequirements = 0; // Set this in Inspector
    
    [SerializeField] GameManager gameManager;
    private bool isCurrentState = false;
    
    [SerializeField] TextMeshProUGUI StepList;

    [SerializeField] TextMeshProUGUI StateNameText;

    [SerializeField] TextMeshProUGUI StepTextPrefab;
      
    // Simple list of requirements that are to checked 
    // These are specific step names in the editor
    [SerializeField]public List<string> requiredObjectsbyName = new List<string>();

     // Dictionary/vector for Object
     // The string is the step's name
    Dictionary<string, bool> requirements_status_dictionary = new Dictionary<string, bool>();

   void Start()
    {
        // I think the load order on Start() should account for what classes are dependent on what for each Start()
        // So the following should be okay to check. If they aren't okay to check, then they would always not exist.


        // On start just check:
        // if the GameManger Singleton exists and if it does set the local reference to that instance
        // if the gameManager gameStates list contains this state (as in it was serialized in the editor of that GameManager Object)
        if(!GameManager.Instance)
        {
            // Debug.Log($"FATAL ERROR: Game Manager Singleton Missing on State Startup!");
        } else
        {
            gameManager = GameManager.Instance;
        }
        


        if(!gameManager.gameStates.Contains(this))
        {
            // Debug.Log($"FATAL ERROR: {stateName} is not known to the GameManager on State Startup! Attempt to add manually");
            gameManager.gameStates.Add(this);
        }else
        {
            
        }

         
    }

    // Initialize from GameManager
    public void Initialize(GameManager manager)
    {
        // Make sure local gameManager reference is set to the singleton that called Initialize()
        gameManager = manager;

        // Make Sure its false since Initialize should only run once from GameManager Instance
        // Game Manager will ensure the first state is set with isCurrentState of that state being true
        isCurrentState = false;
        // Debug.Log($"SettingIsCurrentState to FALSE in state {stateName}");
        // If the steps are expected to be in the serialized field in Unity, then just sort the list as it is.
      // requiredObjectsbyName.Sort();

        if(requiredObjectsbyName.Count() == 0)
        {
            // Has no requirements set
            // Debug.Log($"No Requirements for {stateName}!");
        }

        if(numberOfRequirements != requiredObjectsbyName.Count())
        {
            // Mismatch in editor of number of requirements 
            // Debug.Log($"Mismatch in number of requirements for {stateName}!");
        }

        // Initialize requirements Dictionary with false values 
        foreach (string step in requiredObjectsbyName)
        {
            requirements_status_dictionary.Add(step, false);
            // Debug.Log($"State recognizes (sets false status for) requirement {step} for step dictionary!");
        }
        
        // Double check the keyvalue pair: Should not print anything
        foreach (KeyValuePair<string, bool> requirement in requirements_status_dictionary)
        {
            if(requirements_status_dictionary[requirement.Key])
            {
                // Debug.Log($"State sees incorrectly initialized requirement {requirement.Key} in the dictionary!");
                // requirements_status_dictionary[requirement.Key] = false; 
            }
            
        }

        

    }
    public void DisplayStepList(){
        string output = "";
        foreach(KeyValuePair<string,bool> step in requirements_status_dictionary){
          output += step.Value ? $"\n{step.Key} |COMPLETED|" : $"\n{step.Key} \n|NOT COMPLETED|\n";  
        }
        output = output;
        StepList.SetText(output);

    }
    // Called when this state becomes the active state
    public void OnEnterState()
    {
        // Debug.Log($"Got to OnEnterState from state ${stateName}");
        isCurrentState = true;
        StateNameText.SetText($"STAGE: {stateName}");
        //foreach(KeyValuePair<string,bool> step in requirements_status_dictionarys){
         // GameObject stepTextprefab = Instantiate(stepTextprefab,StepListScrollview.contentContainter);
          //stepTextprefab.SetText($"{step.Key}");
         //// Debug.Log("Writing Step Button");
          ////stepTextprefab.transform.SetParent(StepListScrollview);
          
        //} 
        DisplayStepList();

        // Debug.Log($"Entered state: {stateName}");
    }

    // called when leaving this state
    public void OnExitState()
    {
        isCurrentState = false;
        // Debug.Log($"Set isCurrentState FALSE for state {stateName}");
        
        // Debug.Log($"Exited state: {stateName}");

    }


    // Broadcast Message used by a step
    // Step Name is hardcoded
    public void OnStepCompleted(string step_Name)
    {  
        // Debug.Log($"Completed Step {step_Name} in {stateName}");
        requirements_status_dictionary[step_Name] = true;
        if(isCurrentState) {
          DisplayStepList();
        }
        // Debug.Log($"{stateName} has isCurrentState= {isCurrentState}");

    }

    // Reset state to initial conditions
    // This may cause the steps to break if State Class doesnt tell the step it has to reset through its own reset function
    // Ofc this would be easier if the steps themselves were subclasses of an interaction class that overloads said
    public void ResetState()
    {
        for (int i = 0; i < requiredObjectsbyName.Count(); i++)
        {
            // Set them to false.
            // 
            requirements_status_dictionary[requiredObjectsbyName[i]] = false;
        }     

       // Debug.Log("Reset State"); 
        isCurrentState = false;
    }

    void Update()
    {
      // Debug.Log("In Update");
        if (!isCurrentState) return;
      // Debug.Log("Current State");
        // Check if alssl requirements are met
        if (AllRequirementsMet())
        {
            // Debug.Log("AllReqsMet!!!!");
            CompleteState();
        }
    }

    // Check if all requirements are true
    private bool AllRequirementsMet()
    {
        // Debug.Log("Are All Reqs?");
        //if (requiredObjectsbyName.Count == 0) return true; // No requirements = auto complete
         
        foreach (KeyValuePair<string,bool> entry in requirements_status_dictionary)
        {
            // Debug.Log($"Checking Requirement {entry.Key}");
             if (!entry.Value) return false;
        }
        
        return true;
    }

    // State checks for its own completion and calls CompleteState() to signal to GameManager
    void CompleteState()
    {
        
      // Debug.Log("CompleteState in State Hit!");

      if (gameManager != null)
        {
            // Debug.Log("Signaling GameManager");
            // Signal completion to GameManager
            gameManager.OnStateCompleted(this);
        }else{
          // Debug.Log("GameManager Null :(");
        }
    }

    // Modify Requirements
    // public void SetRequirement(string index, bool value)
    // {
    //     if (index.Length >= 0 && requiredObjectsbyName[index])
    //     {
    //         requiredObjectsbyName[index] = value;
    //         // Debug.Log($"State {stateName}: Requirement {index} set to {value}");
    //     }
    // }

    // public void SetAllRequirements(bool value)
    // {
    //     for (int i = 0; i < requirements.Count; i++)
    //     {
    //         requirements[i] = value;
    //     }
    // }

    // public bool CheckObjectList(List<GameObject> other)
    // {

    //     // Debug.Log("Got to CheckObjectList");
    //     if(requiredObjectTags.Count() == 0){ return true;}
    //     List<string> otherNames = other.ConvertAll<string>(GameObjectToName);

    //     // Debug.Log($"otherTags: {string.Join(",",otherNames)}");

    //     List<string> otherTags = other.ConvertAll<string>(GameObjectToTag);
    //     otherTags.Sort();
    //     // Debug.Log($"otherTags: {string.Join(",",otherTags)}");
    //     // Debug.Log($"requiredObjectTags: {string.Join(",",requiredObjectTags)}");
    //     if (requiredObjectTags.Count != otherTags.Count)
    //     {
    //         return false;
    //     }
    //     return otherTags.All(i=>requiredObjectTags.Contains(i));

    // }

    public static string GameObjectToTag(GameObject obj)
    {
        return obj.gameObject.tag;

    }
    public static string GameObjectToName(GameObject obj)
    {
        return obj.gameObject.name;
    }


}

