using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameOverPopupController : MonoBehaviour
{
    public Transform player;
    public Transform startPosition;
    public bool isRespawned = false;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
    public void RestartScene()
    {
        UI.instance.terrifiedCountText.text = "0";
        UI.instance.shakesCountText.text = "0";
        UI.instance.levelText.text = "0";
        UI.instance.shakesCount = 0;
        UI.instance.slider.value = 0;
        UI.instance.terrifiedCount = 0;
        UI.instance.level = 0;
        PlayerPrefs.DeleteAll();
        player.position = startPosition.position;

        HideCursor();

        gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Rewarded(int id)
    {
        isRespawned = true;
    }

    public void HideCursor()
    {
        if (!YandexGame.EnvironmentData.isMobile)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
