using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public void PauseGame() => Time.timeScale = 0;
    public void ResumeGame() => Time.timeScale = 1;
    public void ToMainMenu() => SceneManager.LoadScene("MainMenu");
}
