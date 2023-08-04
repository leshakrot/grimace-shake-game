using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameOverPopupController : MonoBehaviour
{
    public Transform player;
    public Transform startPosition;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Rewarded(int id)
    {
        player.position = startPosition.position;
        gameObject.SetActive(false);
    }
}
