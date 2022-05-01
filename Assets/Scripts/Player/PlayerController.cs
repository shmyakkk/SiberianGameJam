using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private GameObject bomb;

    private Vector3 inputX;
    private Vector3 inputY;

    private float startTime = 0;
    private float holdTime = 0;

    private bool isStair = false;
    private void Update() => ThrowBomb();
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

    private void ThrowBomb()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            startTime = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            holdTime = Time.time - startTime;

            var force = holdTime * 12;
            if (force > 10) force = 10;

            var currentBomb = Instantiate(bomb, gameObject.transform.position + Vector3.right, bomb.transform.rotation);
            currentBomb.GetComponent<Bomb>().Force = force;

            startTime = 0;
            holdTime = 0;
        }
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
