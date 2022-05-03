using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    protected Image bar;
    private float currentValue;

    protected float MaxValue { get; set; } = 100;
    public float CurrentValue { get => currentValue; set => currentValue = value; }

    protected void SetDefaultValue()
    {
        bar = GetComponent<Image>();
        CurrentValue = 0;
    }
}
