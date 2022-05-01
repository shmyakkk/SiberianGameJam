using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;

    private int dayTime = 10;
    private int nightTime = 10;

    public int DayTime { get => dayTime; }
    public int NightTime { get => nightTime; }

    private void Start()
    {
        GlobalEventManager.OnStartedDay.AddListener(StartDay);
        GlobalEventManager.OnStartedNight.AddListener(StartNight);
        StartCoroutine(DayTimer(DayTime));
        GlobalEventManager.SendStartedDay();
    }

    private void StartDay()
    {
        Debug.Log("Start Day");
    }

    private void StartNight()
    {
        Debug.Log("Start Night");
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
