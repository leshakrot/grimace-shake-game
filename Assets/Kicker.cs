using UnityEngine;
using YG;

public class Kicker : MonoBehaviour
{
    public GameObject gameOverPopup;
    public void Attack()
    {
        if (gameOverPopup != null)
        {
            gameOverPopup.SetActive(true);
            if (!YandexGame.EnvironmentData.isMobile)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void CloseTutor(GameObject tutor)
    {
        if (!YandexGame.EnvironmentData.isMobile)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        tutor.SetActive(false);
    }
}
