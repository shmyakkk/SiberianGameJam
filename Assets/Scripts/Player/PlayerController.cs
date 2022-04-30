using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    private Vector3 inputX;
    private Vector3 inputY;

    private bool isStair = false;

    private void FixedUpdate()
    {
        PlayerMoveX();
        PlayerMoveY();
    }
    private void PlayerMoveX()
    {
        inputX = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        gameObject.transform.Translate(speed * Time.deltaTime * inputX);
    }

    private void PlayerMoveY()
    {
        if (isStair) inputY = new Vector3(0, Input.GetAxis("Vertical"), 0);
        gameObject.transform.Translate(speed * Time.deltaTime * inputY);
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
