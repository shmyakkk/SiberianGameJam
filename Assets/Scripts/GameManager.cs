using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> nature;

    public int NightTime { get; } = 10;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        Time.timeScale = 1;

        GlobalEventManager.OnStartedNight.AddListener(StartNightTimer);

        InvokeRepeating(nameof(RandomSound), 5, Random.Range(10, 15));
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
}
