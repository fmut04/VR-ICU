using UnityEngine;
using UnityEngine.Events;

public class ConfirmButton : MonoBehaviour
{
    public UnityEvent OnConfirmPressed;

    public void Press()
    {
        // Debug.Log("IV Pump Confirmed / Started");
        OnConfirmPressed.Invoke();
    }
}
