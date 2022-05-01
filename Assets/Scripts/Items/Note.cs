using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : BaseItem
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Ура! Записка!");
    }
}
