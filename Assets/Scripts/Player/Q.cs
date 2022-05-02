using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q : MonoBehaviour
{
    private Image bar;
    private bool available = false;

    public float WaitTime { get; set; } = 10f;

    private void Start()
    {
        GlobalEventManager.OnEnterQ.AddListener(SetAvailable);
        GlobalEventManager.OnRestartQ.AddListener(RestartQ);

        bar = GetComponent<Image>();
        bar.fillAmount = 0;
    }
    private void Update()
    {
        if (available) MoveImage();
    }
    private void MoveImage() => bar.fillAmount -= 1.0f / WaitTime * Time.deltaTime;
    private void SetAvailable()
    {
        bar.fillAmount = 1;
        available = true;
    }

    private void RestartQ() => available = false;
}
