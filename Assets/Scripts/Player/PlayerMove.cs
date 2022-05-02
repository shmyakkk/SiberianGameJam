using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody playerRB;

    private bool isStair = false;
    private bool useStair = false;
    private Vector3 stairPos;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float inputX = useStair ? 0 : Input.GetAxis("Horizontal");

        float inputY = 0;
        if (isStair) inputY = Input.GetAxis("Vertical");

        Vector3 directionVector = new Vector3(inputX, inputY, 0);
        Vector3 rotationVector = new Vector3(-inputX, 0, 0);

        if (inputY != 0 && isStair)
        {
            rotationVector = Vector3.back;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(stairPos.x, transform.position.y, transform.position.z), 3 * Time.deltaTime);
        }

        if (directionVector.magnitude > Mathf.Abs(0.1f))
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotationVector), Time.deltaTime * 10);

        playerRB.velocity = directionVector * speed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground")) useStair = false;

        if (other.CompareTag("Stair"))
        {
            isStair = true;
            playerRB.useGravity = false;

            stairPos = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground")) useStair = true;

        if (other.CompareTag("Stair"))
        {
            isStair = false;
            playerRB.useGravity = true;
        }
    }
}
