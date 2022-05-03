using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject q;
    private void Start()
    {
        GlobalEventManager.OnStartedDay.AddListener(QDay);
        GlobalEventManager.OnStartedNight.AddListener(QNight);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseUnpause();
    }
    private void PauseUnpause()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ToMainMenu() => SceneManager.LoadScene("MainMenu");

    private void QDay() => q.SetActive(false);
    private void QNight() => q.SetActive(true);
}
