using UnityEngine;
using YG;
using TMPro;
using System.Collections;

public class CountdownTimerUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    public TextMeshProUGUI timerText;

    private bool isTimerRunning = false;
    private float timer;
    private float initialCountdownTime = 5f;
    private float interval = 60f;

    void Start()
    {
        //StartCountdown();
        StartCoroutine(Countdown());
    }

    void Update()
    {
        Debug.Log(isTimerRunning);
        if (isTimerRunning)
        {
            timerText.text = "�� ������ ������� " + Mathf.CeilToInt(timer).ToString() + " ���";
        }
        else timerText.text = "";
        if (gameOverPanel.activeSelf)
        {
            timerText.gameObject.SetActive(false);
        }
    }

    void StartCountdown()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            timer = initialCountdownTime; // ��������� ���������� �������� �������
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        while (timer > 0f)
        {
            yield return null; // ���� ���� ����

            // ��������� ������ �� ����� ����� ����� �������
            timer -= Time.deltaTime;
        }

        // ������ ����������, ��������� ������ ��������
        TimerEndedAction();

        // ���� ��������� �������� ����� ��������� ������� �������
        yield return new WaitForSeconds(interval);
        isTimerRunning = false;
        timerText.gameObject.SetActive(true);
        StartCountdown();

        // ���������� ������ ��� ���������� �������
        timer = initialCountdownTime;
    }

    void TimerEndedAction()
    {
        // ����� �������� ����� ��� ��������� ��������, ������� ������ ��������� ����� ��������� �������.
        Debug.Log("������ ����������! ��������� ������ ��������.");
        if (!gameOverPanel.activeSelf) YandexGame.Instance._FullscreenShow();

        // ���������� ����������� ������� (���� ���������)
        timerText.gameObject.SetActive(false);
    }
}
