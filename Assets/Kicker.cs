using UnityEngine;
using YG;

public class Kicker : MonoBehaviour
{
    public GameObject gameOverPopup; // Ссылка на попап с game over
    public void Attack()
    {
        // Здесь может быть ваш код для нанесения урона игроку
        // Например, если у игрока есть компонент здоровья, можно вызвать метод получения урона у игрока.

        // Показываем попап с game over
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
