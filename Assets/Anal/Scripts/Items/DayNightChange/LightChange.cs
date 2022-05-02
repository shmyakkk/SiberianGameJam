using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;

    private new Light light;

    private void Start()
    {
        light = GetComponent<Light>();

        GlobalEventManager.OnStartedDay.AddListener(StartDayLight);
        GlobalEventManager.OnStartedNight.AddListener(StartNightLight);
    }

    private void StartDayLight() => light.color = dayColor;

    private void StartNightLight() => light.color = nightColor;
}
