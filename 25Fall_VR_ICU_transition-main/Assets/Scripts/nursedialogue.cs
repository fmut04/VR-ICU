using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NurseDialogue : MonoBehaviour
{
    // ── Optional distance gating ─────────────────────────────────────────────
    [Header("Player (optional)")]
    public Transform playerCamera; // CenterEyeAnchor
    public float triggerDistance = 2f; // Set to 0 to always show dialogue

    // ── UI References ─────────────────────────────────────────────────────────
    [Header("Panels")]
    public GameObject dialoguePanel; // main dialogue panel
    public GameObject buttonsPanel; // holds buttons

    [Header("Texts")]
    public TextMeshProUGUI nurse1Text;
    public TextMeshProUGUI nurse2Text;

    [Header("Buttons")]
    public Button optionAButton;
    public TextMeshProUGUI optionAText;

    public Button optionBButton;
    public TextMeshProUGUI optionBText;


    private string _buttonACallback;
    private string _buttonBCallback;
    // ── Dialogue States ──────────────────────────────────────────────────────
    enum Step
    {
        Start_Dialogue,
        RN1_Report,
        Ask_WhyCentralLine,
        Ans_WhyCentralLine,
        Ask_WhenPlaced,
        Ans_WhenPlaced,
        Ask_DressingChanged,
        Ans_DressingChanged,
        Ask_BloodReturn,
        Ans_BloodReturn,
        Ask_DeclotAttempts,
        Ans_DeclotAttempts,
        Ask_CHGBath,
        Ans_CHGBath,
        LeadInto_TubingWork
    }

    Step step;

    // ─────────────────────────────────────────────────────────────────────────
    void Start()
    {
        // Debug.Log("NurseDialogue: START (No hygiene mode)");

        // Turn on dialogue immediately
       // if (dialoguePanel)
       dialoguePanel.SetActive(true);
        
        //if (buttonsPanel)
     //   buttonsPanel.SetActive(true);
       // ClearButtons();
      //  SayRN1("Text!");
     //   SayRN2("Text!");

        // Start the first part of the dialogue
     //   Invoke(nameof(DialogueStart), 0);
    }

    void OnEnable()
    {
      dialoguePanel.SetActive(true);
      buttonsPanel.SetActive(true);

      DialogueStart();

    }

    // ─────────────────────────────────────────────────────────────────────────
    void Update()
    {
        // OPTIONAL: gate dialogue by distance
        if (playerCamera && triggerDistance > 0f)
        {
            Vector3 playerXZ = new Vector3(playerCamera.position.x, 0, playerCamera.position.z);
            Vector3 npcXZ = new Vector3(transform.position.x, 0, transform.position.z);
            float d = Vector3.Distance(playerXZ, npcXZ);

            dialoguePanel.SetActive(d <= triggerDistance);
        }
    }
    
    void SetButtonCallbacks(string callbackA, string callbackB)
    {
      // Debug.Log("Reached SetButtonCallback");
        optionAButton.BroadcastMessage("SetCallbackFunction", callbackA);
        optionBButton.BroadcastMessage("SetCallbackFunction", callbackB);  
    }

  
    // ── Dialogue Flow ──────────────────────────────────────────────────────────
    //
   public void DialogueStart()
   {
       SayRN1("Press the Button to begin the bedside report upon reaching the proper state of the program");

      SetButtons("Start Report", "");
      SetButtonCallbacks("Go_Report","");

   }
   public void Go_Report()
    {
        step = Step.RN1_Report;
        // Debug.Log("Got to Go Report");
        SayRN1(
            "Patient A is a 78-year-old female with a history of common variable immunodeficiency. " +
            "She was admitted to the MICU 10 days ago for ARDS requiring intubation in the setting of COVID pneumonia " +
            "and was found to have MRSA bacteremia. A triple lumen central line was inserted in the right subclavian.\n\n" +
            "Do you have any questions?"
        );

        SetButtons("Ask: Why central line?", "When was it placed?");
       
       SetButtonCallbacks("Go_Ask_WhyCentralLine","Go_Ask_WhenPlaced");
//        optionAButton.onClick.AddListener(Go_Ask_WhyCentralLine);
//        optionBButton.onClick.AddListener(Go_Ask_WhenPlaced);
    }

    public void Go_Ask_WhyCentralLine()
    {
        step = Step.Ask_WhyCentralLine;

        SayRN2("Why does this patient have a central line?");
  //      ClearButtons();

        Invoke(nameof(Go_Ans_WhyCentralLine), .6f);
    }

    public void Go_Ans_WhyCentralLine()
    {
        step = Step.Ans_WhyCentralLine;

        SayRN1("The patient had difficult peripheral IV access and requires six weeks of antibiotics.");

        SetButtons("When was it placed?", "When was the dressing last changed?");
        SetButtonCallbacks("Go_Ask_WhenPlaced","Go_Ask_DressingChanged");
//        optionAButton.onClick.AddListener(Go_Ask_WhenPlaced);
//        optionBButton.onClick.AddListener(Go_Ask_DressingChanged);
    }

    public void Go_Ask_WhenPlaced()
    {
        step = Step.Ask_WhenPlaced;
        SayRN2("When was the central line placed?");
    //    ClearButtons();

        Invoke(nameof(Go_Ans_WhenPlaced), .6f);
    }

    public void Go_Ans_WhenPlaced()
    {
        step = Step.Ans_WhenPlaced;

        SayRN1("Seven days ago.");

        SetButtons("When was the dressing last changed?", "Blood return on all lumens?");
        SetButtonCallbacks("Go_Ask_DressingChanged","Go_Ask_BloodReturn");

        //optionAButton.onClick.AddListener(Go_Ask_DressingChanged);
        //optionBButton.onClick.AddListener(Go_Ask_BloodReturn);
    }

    public void Go_Ask_DressingChanged()
    {
        step = Step.Ask_DressingChanged;
        SayRN2("When was the dressing last changed?");
    //    ClearButtons();

        Invoke(nameof(Go_Ans_DressingChanged), .6f);
    }

    public void Go_Ans_DressingChanged()
    {
        step = Step.Ans_DressingChanged;

        SayRN1("The original dressing from insertion seven days ago remains in place.");

        SetButtons("Blood return on all lumens?", "Attempts to declot?");
        SetButtonCallbacks("Go_Ask_BloodReturn","Go_Ask_DeclotAttempts");
//        optionAButton.onClick.AddListener(Go_Ask_BloodReturn);
//        optionBButton.onClick.AddListener(Go_Ask_DeclotAttempts);
    }

    public void Go_Ask_BloodReturn()
    {
        step = Step.Ask_BloodReturn;
        SayRN2("Does the line have good blood return on all lumens?");
       // ClearButtons();

        Invoke(nameof(Go_Ans_BloodReturn), 0.6f);
    }

    public void Go_Ans_BloodReturn()
    {
        step = Step.Ans_BloodReturn;

        SayRN1("Two lumens have good blood return; the third isn’t flushing.");

        SetButtons("Attempts to declot?", "CHG bath & linens in last 24h?");
        SetButtonCallbacks("Go_Ask_DeclotAttempts","Go_Ask_CHGBath");
        //optionAButton.onClick.AddListener(Go_Ask_DeclotAttempts);
        //optionBButton.onClick.AddListener(Go_Ask_CHGBath);
    }

    public void Go_Ask_DeclotAttempts()
    {
        step = Step.Ask_DeclotAttempts;
        SayRN2("Have any attempts been made to declot the lumen?");
        //ClearButtons();

        Invoke(nameof(Go_Ans_DeclotAttempts), 0.6f);
    }

    public void Go_Ans_DeclotAttempts()
    {
        step = Step.Ans_DeclotAttempts;

        SayRN1("No.");

        SetButtons("CHG bath & linens in last 24h?", "Proceed to antibiotic setup");
        
        SetButtonCallbacks("Go_Ask_CHGBath","Go_LeadIntoTubing");


        optionAButton.onClick.AddListener(Go_Ask_CHGBath);
        optionBButton.onClick.AddListener(Go_LeadIntoTubing);
    }

    public void Go_Ask_CHGBath()
    {
        step = Step.Ask_CHGBath;
        SayRN2("Was the patient given a CHG bath within the last 24 hours?");
        //ClearButtons();

        Invoke(nameof(Go_Ans_CHGBath), 0.6f);
    }




    public void Go_Ans_CHGBath()
    {
        step = Step.Ans_CHGBath;

        SayRN1("Yes — the patient received a CHG bath and linens were changed this morning.");

        SetButtons("Proceed to antibiotic setup", "");
        
        SetButtonCallbacks("FinishDialogue","");

      //  optionAButton.onClick.AddListener(Go_LeadIntoTubing);
    }

    public void Go_LeadIntoTubing()
    {
        step = Step.LeadInto_TubingWork;

        SayRN1("Let’s hang the new bag of IV antibiotic, Zosyn. Let's replace the previous empty bag.");

        SetButtons("Finish Bedside Report", "");

        SetButtonCallbacks("FinishDialogue","");
        
      
    }


    public void FinishDialogue(){
        
      BroadcastMessage("OnInteractionCompleted");

    }
    
    // ── UI Helpers ───────────────────────────────────────────────────────────
    void SayRN1(string text)
    {
        if (nurse1Text)
            nurse1Text.text = text;
    }

    void SayRN2(string text)
    {
        if (nurse2Text)
            nurse2Text.text = text;
    }

    void SetButtons(string a, string b)
    {
        if (!buttonsPanel) return;

        buttonsPanel.SetActive(true);

      //  optionAButton.onClick.RemoveAllListeners();
      // optionBButton.onClick.RemoveAllListeners();

        optionAText.text = a;
        //optionAButton.gameObject.SetActive(!string.IsNullOrWhiteSpace(a));

        optionBText.text = b;
       // optionBButton.gameObject.SetActive(!string.IsNullOrWhiteSpace(b));
    }

    void ClearButtons()
    {
      //  optionAButton.onClick.RemoveAllListeners();
      //  optionBButton.onClick.RemoveAllListeners();

        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        if (buttonsPanel)
            buttonsPanel.SetActive(false);
    }
}
