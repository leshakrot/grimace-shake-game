using UnityEngine;
using Cinemachine;

public class CineTouch : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cineCam;
    [SerializeField] TouchField touchField;
    [SerializeField] JoystickBehaviour joyStick;

    public float SenstivityX = 2f;
    public float SenstivityY = 2f;

    private bool isJoyStickActive = false;

    void Start()
    {
        // Подписываемся на события джойстика
        joyStick.OnJoyStickPressedEvent += OnJoyStickPressed;
        joyStick.OnJoyStickReleasedEvent += OnJoyStickReleased;
    }

    void OnDestroy()
    {
        // Отписываемся от событий джойстика при уничтожении объекта
        joyStick.OnJoyStickPressedEvent -= OnJoyStickPressed;
        joyStick.OnJoyStickReleasedEvent -= OnJoyStickReleased;
    }

    void Update()
    {
        if (!isJoyStickActive)
        {
            cineCam.m_XAxis.Value += touchField.TouchDist.x * 200 * SenstivityX * Time.deltaTime;
            cineCam.m_YAxis.Value += touchField.TouchDist.y * SenstivityY * Time.deltaTime;
        }
    }

    void OnJoyStickPressed()
    {
        isJoyStickActive = true;
    }

    void OnJoyStickReleased()
    {
        isJoyStickActive = false;
    }
}
