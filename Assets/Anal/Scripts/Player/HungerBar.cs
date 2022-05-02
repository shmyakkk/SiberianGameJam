using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : PlayerBars
{
    private void Start()
    {
        SetDefaultValue();
        StartCoroutine(IncreaseHunger());
    }
    private void Update()
    {
        bar.fillAmount = CurrentValue / MaxValue;
    }
    private IEnumerator IncreaseHunger()
    {
        while (CurrentValue < MaxValue)
        {
            yield return new WaitForSeconds(5);
            CurrentValue += 5f;
        }
    }
}
