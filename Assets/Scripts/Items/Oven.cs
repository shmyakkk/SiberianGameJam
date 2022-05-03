using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : BaseItem
{
    [SerializeField] private OvenBar ovenBar;

    public override void Enter()
    {
        base.Enter();
        Debug.Log(12);
        ovenBar.CurrentValue = 100;
        Available = true;
    }
}
