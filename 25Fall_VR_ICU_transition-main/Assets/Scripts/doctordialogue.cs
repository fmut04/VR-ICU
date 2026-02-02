using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoctorDialogue : MonoBehaviour
{
    [Header("Player & Range")]
    public Transform playerCamera; // drag XR Rig > Camera Offset > Main Camera
    public float triggerDistance = 1.2f; // meters

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI doctorText;

    public Button optionAButton;
    public TextMeshProUGUI optionAText;

    public Button optionBButton;
    public TextMeshProUGUI optionBText;

    bool isShowing = false;

    void Start()
    {
        // Hide everything at start
        if (dialoguePanel) dialoguePanel.SetActive(false);

        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerCamera) return;

        float d = Vector3.Distance(transform.position, playerCamera.position);
        bool near = d < triggerDistance;

        if (near && !isShowing)
        {
            isShowing = true;
            ShowGreeting(); // wires up buttons
        }
        else if (!near && isShowing)
        {
            isShowing = false;
            HideAndReset();
        }
    }

    void ShowGreeting()
    {
        dialoguePanel.SetActive(true);

        doctorText.text = "Hello, we’re preparing for an ICU transfer. Review CLABSI steps?";

        SetOptions("Yes, let’s review", "Not right now");

        optionAButton.gameObject.SetActive(true);
        optionBButton.gameObject.SetActive(true);

        optionAButton.onClick.RemoveAllListeners();
        optionAButton.onClick.AddListener(StartStep1);

        optionBButton.onClick.RemoveAllListeners();
        optionBButton.onClick.AddListener(() =>
        {
            doctorText.text = "Okay, but skipping steps puts patients at risk.";
            SetOptions("Close", "");
            optionBButton.gameObject.SetActive(false);

            optionAButton.onClick.RemoveAllListeners();
            optionAButton.onClick.AddListener(HideAndReset);
        });
    }

    void StartStep1()
    {
        doctorText.text = "First: perform hand hygiene before touching the catheter.";

        SetOptions("Wash hands now", "Skip hand washing");

        optionBButton.gameObject.SetActive(true);

        optionAButton.onClick.RemoveAllListeners();
        optionAButton.onClick.AddListener(() =>
        {
            doctorText.text = "Good job. Hand hygiene is the first defense.";
            Invoke(nameof(StartStep2), 1.0f);
        });

        optionBButton.onClick.RemoveAllListeners();
        optionBButton.onClick.AddListener(() =>
        {
            doctorText.text = "That increases infection risk—always sanitize first.";
            Invoke(nameof(StartStep2), 1.0f);
        });
    }

    void StartStep2()
    {
        doctorText.text = "Next: check the catheter site for redness or swelling.";

        SetOptions("Inspect the site", "Ignore the site");

        optionAButton.onClick.RemoveAllListeners();
        optionAButton.onClick.AddListener(() =>
        {
            doctorText.text = "No issues detected. Safe to proceed.";
            Invoke(nameof(StartStep3), 1.0f);
        });

        optionBButton.onClick.RemoveAllListeners();
        optionBButton.onClick.AddListener(() =>
        {
            doctorText.text = "Site checks are critical—don’t skip them.";
            Invoke(nameof(StartStep3), 1.0f);
        });
    }

    void StartStep3()
    {
        doctorText.text = "Finally: maintain sterile technique when reconnecting lines.";

        SetOptions("Finish", "Finish");

        optionAButton.onClick.RemoveAllListeners();
        optionBButton.onClick.RemoveAllListeners();

        optionAButton.onClick.AddListener(HideAndReset);
        optionBButton.onClick.AddListener(HideAndReset);
    }

    void HideAndReset()
    {
        CancelInvoke();

        optionAButton.onClick.RemoveAllListeners();
        optionBButton.onClick.RemoveAllListeners();

        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        if (dialoguePanel)
            dialoguePanel.SetActive(false);
    }

    void SetOptions(string a, string b)
    {
        optionAText.text = a;
        optionBText.text = b;

        optionBButton.gameObject.SetActive(!string.IsNullOrEmpty(b));
    }
}
