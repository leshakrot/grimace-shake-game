using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // События для определения активности джойстика
    public delegate void OnJoyStickPressed();
    public delegate void OnJoyStickReleased();
    public event OnJoyStickPressed OnJoyStickPressedEvent;
    public event OnJoyStickReleased OnJoyStickReleasedEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnJoyStickPressedEvent != null)
        {
            OnJoyStickPressedEvent(); // Вызываем событие при нажатии на джойстик
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnJoyStickReleasedEvent != null)
        {
            OnJoyStickReleasedEvent(); // Вызываем событие при отпускании джойстика
        }
    }
}
