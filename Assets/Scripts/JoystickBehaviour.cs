using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // ������� ��� ����������� ���������� ���������
    public delegate void OnJoyStickPressed();
    public delegate void OnJoyStickReleased();
    public event OnJoyStickPressed OnJoyStickPressedEvent;
    public event OnJoyStickReleased OnJoyStickReleasedEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnJoyStickPressedEvent != null)
        {
            OnJoyStickPressedEvent(); // �������� ������� ��� ������� �� ��������
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnJoyStickReleasedEvent != null)
        {
            OnJoyStickReleasedEvent(); // �������� ������� ��� ���������� ���������
        }
    }
}
