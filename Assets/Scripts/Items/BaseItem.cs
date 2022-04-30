using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public virtual void Enter() { }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Enter();
        }
    }
}
