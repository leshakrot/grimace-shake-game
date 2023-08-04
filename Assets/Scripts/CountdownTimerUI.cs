using UnityEngine;
using YG;
using TMPro;
using System.Collections;

public class CountdownTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private bool isTimerRunning = false;
    private float timer;
    private float initialCountdownTime = 5f;
    private float interval = 60f;
    private bool isFirstRun;

    void Start()
    {
        //StartCountdown();
        StartCoroutine(Countdown());
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timerText.text = "�� ������ ������� " + Mathf.CeilToInt(timer).ToString() + " ���";
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
        YandexGame.Instance._FullscreenShow();

        // ���������� ����������� ������� (���� ���������)
        timerText.gameObject.SetActive(false);
    }
}
