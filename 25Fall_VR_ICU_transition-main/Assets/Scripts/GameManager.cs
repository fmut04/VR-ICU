using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton: Reference from anywhere using GameManager.Instance
    public static GameManager Instance { get; private set; }

    // Drag your State GameObjects here in Inspector
    // Could implement a find function
    [SerializeField] public List<State> gameStates = new List<State>();
    
     
    // Tracks which state is active
    private int currentStateIndex = -1;
    
    private State CurrentState => (currentStateIndex >= 0 && currentStateIndex < gameStates.Count) 
        ? gameStates[currentStateIndex] 
        : null;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Has to be singleton, destroy all others
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Initialize all states in the state list
        foreach (var state in gameStates)
        {
            if (state != null)
            {
                state.Initialize(this);
            }
        }
        
        // Start with first state
        if (gameStates.Count > 0)
        {
            NextState();
        }
    }

    // Advance to next state
    public void NextState()
    {
        // Exit current state
        if (CurrentState != null)
        {
            // Debug.Log("CurrentState is Null in NextState\n");
            CurrentState.OnExitState();
        }

        // Advance to next state
        currentStateIndex++;
        // Debug.Log($"currentStateIndex: {currentStateIndex}");
        //CurrentState.OnEnterState();
        if (currentStateIndex >= gameStates.Count)
        {
            // Debug.Log("All states completed!");
            return;
        }
        

        // Enter new state
        CurrentState.OnEnterState();
        
    }

    // Called by State when requirements are met
    // Calls NextState() to sequentially go to next state
    public void OnStateCompleted(State completedState)
    {
        // Debug.Log("OnStateCompleted Hit");  
      // Verify this is the current state
        if (completedState == CurrentState)
        {
            // Debug.Log($"State {currentStateIndex} completed");
            NextState();
        }
        else
        {
            // Debug.LogWarning("Non-current state tried to complete!");
        }
    }

    
   
    public void Reset() // Named class
    {
        // Not sure what to put here yet
    }

    // Reset the currenet state: called from handler func?
    public void ResetCurrentState()
    {
        // Reset just the current state's requirements
        if (CurrentState != null)
        {
            CurrentState.ResetState();
            // Debug.Log($"Reset current state {currentStateIndex}");
        }
    }

    // This resets the entire gameManager, but it would likely just break the program.
    public void RestartFromBeginning()
    {
        // Exit current state
        if (CurrentState != null)
        {
            CurrentState.OnExitState();
        }
        
        // Reset to beginning
        currentStateIndex = -1;
        
        // Reset all states
        foreach (var state in gameStates)
        {
            if (state != null)
            {
                state.ResetState();
            }
        }
        
        // Start first state
        NextState();
    }

    // Add a new state to the GameManager's state list
    public void RegisterState(State state)
    {
        // Debug.Log($"RegisteringState: {state}");
        if (!gameStates.Contains(state))
        {
            gameStates.Add(state);
        }
    }
}

