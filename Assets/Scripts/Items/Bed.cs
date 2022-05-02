using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : BaseItem
{
    private void Start() => GlobalEventManager.OnStartedDay.AddListener(SetBedAvailable);
    public override void Enter()
    {
        base.Enter();
        GlobalEventManager.SendStartedNight();
        Debug.Log("���! C��!");
    }
    private void SetBedAvailable() => Available = true;
}
