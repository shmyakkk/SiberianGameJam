using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OvenBar : PlayerBars
{
    public float fuel = 0f;
    void Start()
    {
        SetDefaultValue();
        StartCoroutine(IncreaseFuel());
    }


    void Update()
    {
        bar.fillAmount = fuel;
    }
    private IEnumerator IncreaseFuel()
    {
        while (fuel <= 1f)
        {
            yield return new WaitForSeconds(1);
            fuel += 0.02f;
            Debug.Log(fuel);
        }
    }
}
