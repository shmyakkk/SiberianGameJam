using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : BaseItem
{
    [SerializeField] private OvenBar fuel;
    private void Start()
    {
        fuel = GetComponent<OvenBar>();
    }
    private void Update()
    {
        fuel.fuel++;
    }

}
