using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 inputX;
    [SerializeField] private float speed = 3;

    private void FixedUpdate()
    {
        PlayerMoveX();
    }
    private void PlayerMoveX()
    {
        inputX = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        gameObject.transform.Translate(speed * Time.deltaTime * inputX);
    }
}
