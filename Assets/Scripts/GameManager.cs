using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;

    public int DayTime { get; } = 10;
    public int NightTime { get; } = 10;

    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(DayTimer(DayTime));
    }


    private IEnumerator DayTimer(int time)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
        if (time == 0)
        {
            StartCoroutine(NightTimer(NightTime));
            GlobalEventManager.SendStartedNight();
        }
    }

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
            StartCoroutine(DayTimer(DayTime));
            
            GlobalEventManager.SendStartedDay();
        }
    }
}
