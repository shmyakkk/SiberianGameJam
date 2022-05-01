using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public bool IsUsed { get; set; } = false;

    public virtual void Enter() => IsUsed = true;

    private void OnTriggerStay(Collider other)
    {
        if (!IsUsed && Input.GetKey(KeyCode.E)) Enter();
    }
}
