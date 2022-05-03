using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q : MonoBehaviour
{
    private Image bar;
    public bool isEnter = false;

    public float WaitTime { get; set; } = 10f;

    private void Start()
    {
        GlobalEventManager.OnEnterQ.AddListener(EnterQ);
        GlobalEventManager.OnReloadQ.AddListener(ReloadQ);

        bar = GetComponent<Image>();
        bar.fillAmount = 0;
    }
    private void Update()
    {
        if (isEnter) MoveImage();
    }
    private void MoveImage() => bar.fillAmount -= 1.0f / WaitTime * Time.deltaTime;
    private void EnterQ()
    {
        bar.fillAmount = 1;
        isEnter = true;
    }
    private void ReloadQ() => isEnter = false;
}
