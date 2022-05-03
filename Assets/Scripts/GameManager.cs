using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private GameObject granny;
    [SerializeField] private GameObject men;

    [Header(" ")]
    [SerializeField] private GameObject q;

    [Header(" ")]
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> nature;
    [SerializeField] private AudioClip dayMusic;
    [SerializeField] private AudioClip nightMusic;
    public int NightTime { get; } = 60;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        Time.timeScale = 1;

        GlobalEventManager.OnStartedDay.AddListener(PlayDayMusic);
        GlobalEventManager.OnStartedNight.AddListener(StartNightTimer);
        GlobalEventManager.OnStartedNight.AddListener(PlayNightMusic);

        GlobalEventManager.OnStartedDay.AddListener(SetActiveMen);
        GlobalEventManager.OnStartedNight.AddListener(SetActiveGranny);

        InvokeRepeating(nameof(RandomSound), 5, Random.Range(10, 15));

        GlobalEventManager.SendStartedDay();
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

    private void RandomSound()
    {
        audioSource.PlayOneShot(nature[Random.Range(0, nature.Count)]);
    }

    private void PlayDayMusic() => audioSource.PlayOneShot(dayMusic);
    private void PlayNightMusic() => audioSource.PlayOneShot(nightMusic);

    private void SetActiveGranny()
    {
        men.SetActive(false);
        granny.transform.position = men.transform.position;
        granny.SetActive(true);
        q.SetActive(true);
    }

    private void SetActiveMen()
    { 
        granny.SetActive(false);
        men.transform.position = granny.transform.position;
        men.SetActive(true);
        q.SetActive(false);
    }
}
