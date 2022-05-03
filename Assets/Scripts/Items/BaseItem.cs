using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public bool Available { get; set; } = true;

    //private void Awake() => SetBoxCollider();

    private void SetBoxCollider()
    {
        var boxCol = gameObject.AddComponent<BoxCollider>();
        boxCol.isTrigger = true;
        boxCol.size += new Vector3(0.3f, 0.3f, 10f);
    }
    public virtual void Enter() => Available = false;

    private void OnTriggerStay(Collider other)
    {
        if (Available && Input.GetKey(KeyCode.E)) Enter();
    }
}
