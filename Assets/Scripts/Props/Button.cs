using UnityEngine;
using UnityEngine.Events;

namespace Props
{
    public class Button : MonoBehaviour
    {
        public UnityEvent<Button> OnPressed;
    
        public void PressButton()
        {
            OnPressed?.Invoke(this);
        }
    }
}
