using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OvenBar : PlayerBars
{
    void Start()
    {
        SetDefaultValue();
        StartCoroutine(IncreaseFuel());
        CurrentValue = MaxValue;
    }

    void Update()
    {
        bar.fillAmount = CurrentValue / MaxValue;
    }
    private IEnumerator IncreaseFuel()
    {
        while (CurrentValue < MaxValue)
        {
            yield return new WaitForSeconds(5);
            CurrentValue -= 5f;
        }
    }
}
