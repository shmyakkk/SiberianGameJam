using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;

    public int NightTime { get; } = 10;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        Time.timeScale = 1;

        GlobalEventManager.OnStartedNight.AddListener(StartNightTimer);
    }

    private void StartNightTimer() => StartCoroutine(NightTimer(NightTime));

    private IEnumerator NightTimer(int time)
    {
        while (time > 0)
        {
            timer.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }
        if (time == 0)
        {
            timer.text = "";
            GlobalEventManager.SendStartedDay();
        }
    }
}
