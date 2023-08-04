using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPopupController : MonoBehaviour
{
    public Transform player;
    public Transform startPosition;
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TryAgainForReward()
    {
        player.position = startPosition.position;
        gameObject.SetActive(false);
    }
}
