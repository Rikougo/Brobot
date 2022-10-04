using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent<Button> OnPressed;
    
    public void PressButton()
    {
        OnPressed?.Invoke(this);
    }
}
