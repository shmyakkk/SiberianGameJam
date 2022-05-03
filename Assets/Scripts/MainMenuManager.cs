using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> intro;
    [SerializeField] private Image imageIntro;
    private int index;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        index = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) imageIntro.sprite = intro[++index];

        if (index == intro.Count - 1) StartGame();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
