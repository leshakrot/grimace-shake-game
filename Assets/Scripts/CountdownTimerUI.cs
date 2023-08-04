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
            timerText.text = "До показа рекламы " + Mathf.CeilToInt(timer).ToString() + " сек";
        }
    }

    void StartCountdown()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            timer = initialCountdownTime; // Установка начального значения таймера
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        while (timer > 0f)
        {
            yield return null; // Ждем один кадр

            // Уменьшаем таймер на время между двумя кадрами
            timer -= Time.deltaTime;
        }

        // Таймер закончился, выполняем нужное действие
        TimerEndedAction();

        // Ждем указанный интервал перед следующим показом таймера
        yield return new WaitForSeconds(interval);
        isTimerRunning = false;
        timerText.gameObject.SetActive(true);
        StartCountdown();

        // Сбрасываем таймер для следующего отсчета
        timer = initialCountdownTime;
    }

    void TimerEndedAction()
    {
        // Здесь вызовите метод или выполните действие, которое должно произойти после окончания таймера.
        Debug.Log("Таймер завершился! Выполняем нужное действие.");
        YandexGame.Instance._FullscreenShow();

        // Остановить отображение таймера (если требуется)
        timerText.gameObject.SetActive(false);
    }
}
