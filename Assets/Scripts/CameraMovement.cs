using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private GameObject granny;
    [SerializeField] private GameObject men;

    private GameObject player;

    [Header(" ")]
    [SerializeField] private Vector3 distanceFromPlayer;

    private void Start()
    {
        GlobalEventManager.OnStartedDay.AddListener(SetMen);
        GlobalEventManager.OnStartedNight.AddListener(SetGranny);
    }
    private void FixedUpdate() => CameraMove();

    private void CameraMove()
    {
        Vector3 positionToGo = player.transform.position + distanceFromPlayer;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, positionToGo, 0.125f);
        transform.position = smoothPosition;
    }

    private void SetGranny() => player = granny;
    private void SetMen() => player = men;
}
