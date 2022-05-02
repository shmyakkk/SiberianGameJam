using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChange : MonoBehaviour
{
    [SerializeField] private GameObject dayView;
    [SerializeField] private GameObject nightView;

    private void Start()
    {
        GlobalEventManager.OnStartedDay.AddListener(ChangeToDayView);
        GlobalEventManager.OnStartedNight.AddListener(ChangeToNightView);
    }
    private void ChangeToDayView()
    {
        Instantiate(dayView, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }

    private void ChangeToNightView()
    {
        Instantiate(nightView, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
