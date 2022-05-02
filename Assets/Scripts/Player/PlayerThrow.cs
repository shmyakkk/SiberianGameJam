using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private GameObject bomb;

    private float startTime = 0;
    private float holdTime = 0;

    private bool availableThrow = true;

    private void Start() => GlobalEventManager.OnEnterQ.AddListener(AvailableThrow);
    private void Update() => ThrowBomb();

    private void ThrowBomb()
    {
        if (availableThrow)
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

                GlobalEventManager.SendEnterQ();
            }
        }
    }

    private void AvailableThrow()
    {
        availableThrow = false;
        StartCoroutine(AvailableThrowTime());
    }

    private IEnumerator AvailableThrowTime()
    {
        yield return new WaitForSeconds(10);
        availableThrow = true;
        GlobalEventManager.SendReloadQ();
    }
}
