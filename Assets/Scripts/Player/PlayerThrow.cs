using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private GameObject bomb;

    private float startTime = 0;
    private float holdTime = 0;

    private Vector3 direction;
    private Vector3 position;

    private void Start() => GlobalEventManager.OnEnterQ.AddListener(AvailableThrow);
    private void Update() => ThrowBomb();

    public ThrowStates CurrentState { get; set; } = ThrowStates.Right;

    public enum ThrowStates
    {
        Left,
        Right,
        Disabled
    }

    private void ThrowBomb()
    {
        if (CurrentState != ThrowStates.Disabled)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                startTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                holdTime = Time.time - startTime;

                var force = holdTime * 20;
                if (force > 15) force = 15;

                if (CurrentState == ThrowStates.Left)
                {
                    direction = new Vector3(-1, 1, 0);
                    position = Vector3.left;
                }

                if (CurrentState == ThrowStates.Right)
                {
                    direction = new Vector3(1, 1, 0);
                    position = Vector3.right;
                }

                var currentBomb = Instantiate(bomb, gameObject.transform.position + position, bomb.transform.rotation);
                currentBomb.GetComponent<Bomb>().Force = force;
                currentBomb.GetComponent<Bomb>().ThrowDirection = direction;

                startTime = 0;
                holdTime = 0;

                GlobalEventManager.SendEnterQ();
            }
        }
    }

    private void AvailableThrow()
    {
        CurrentState = ThrowStates.Disabled;
        StartCoroutine(AvailableThrowTime());
    }

    private IEnumerator AvailableThrowTime()
    {
        yield return new WaitForSeconds(10);

        CurrentState = ThrowStates.Right;
        GlobalEventManager.SendReloadQ();
    }
}
