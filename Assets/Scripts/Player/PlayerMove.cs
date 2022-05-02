using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody playerRB;
    private float speed = 3f;

    private bool isStair = false;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();   
    }
    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = 0;

        if (isStair)
        {
            inputY = Input.GetAxis("Vertical");
        }

        Vector3 directionVector = new Vector3(inputX, inputY, 0);
        Vector3 rotationVector = new Vector3(-inputX, 0, 0);

        if (inputY != 0)
        {
            rotationVector = Vector3.back;
        }

        if (directionVector.magnitude > Mathf.Abs(0.1f))
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotationVector), Time.deltaTime * 10);

        playerRB.velocity = Vector3.ClampMagnitude(directionVector, 1) * speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            isStair = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            isStair = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
