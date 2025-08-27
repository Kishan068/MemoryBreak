using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CountdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // 或 public Text countdownText;

    private Coroutine countdownRoutine;

    public void StartCountdown(float time)
    {
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        countdownRoutine = StartCoroutine(CountdownCoroutine(time));
    }

IEnumerator CountdownCoroutine(float time)
{
    countdownText.gameObject.SetActive(true);

    while (time > 0)
    {
        countdownText.text = time.ToString("F1"); // 显示小数1位，如 4.9
        yield return new WaitForSeconds(0.1f);
        time -= 0.1f;
    }

    countdownText.text = "0.0";
    yield return new WaitForSeconds(0.3f);
    countdownText.gameObject.SetActive(false);
}


    public void Hide()
    {
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        countdownText.gameObject.SetActive(false);
    }
}
