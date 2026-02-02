using UnityEngine;
using UnityEngine.Events;

public class PowerButton : MonoBehaviour
{
    public UnityEvent OnPowerPressed;

    public void Press()
    {
        // Debug.Log("IV Pump Powered On");
        OnPowerPressed.Invoke();
    }
}
