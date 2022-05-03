using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
    public void StartIntro()
    {
        SceneManager.LoadScene("Intro");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
