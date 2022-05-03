using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : BaseItem
{

    private void Start()
    {

    }
    private void FixedUpdate()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && OvenBar.fuel > 0f)
        {
            OvenBar.fuel -= 0.05f;
        }
    }
}
