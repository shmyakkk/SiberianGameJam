using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator playerAnim;
    [Header(" ")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> steps;
    [SerializeField] private List<AudioClip> stepsNight;
    [SerializeField] private AudioClip ladder;
    [SerializeField] private AudioClip ladderNight;
    [SerializeField] private AudioClip voiceInsane;

    private Rigidbody playerRB;
    private PlayerThrow playerThrow;

    private bool isStair = false;
    private bool useStair = false;
    private Vector3 stairPos;

    private bool isDay = true;

    private void Start()
    {
        GlobalEventManager.OnStartedDay.AddListener(StartDaySteps);
        GlobalEventManager.OnStartedNight.AddListener(StartNightSteps);

        playerRB = GetComponent<Rigidbody>();
        playerThrow = GetComponent<PlayerThrow>();
    }
    private void Update() => MoveAndRotate();

    private void StartDaySteps() => isDay = true;
    private void StartNightSteps() => isDay = false;
    private void PlayStepsSound()
    {
        if (!audioSource.isPlaying)
        {
            if (isDay)
                audioSource.PlayOneShot(steps[Random.Range(0, steps.Count)]);
            else
                audioSource.PlayOneShot(stepsNight[Random.Range(0, stepsNight.Count)]);
        }
    }
    private void PlayLadderSound()
    {
        if (!audioSource.isPlaying)
        {
            if (isDay)
                audioSource.PlayOneShot(ladder);
            else
                audioSource.PlayOneShot(ladderNight);
        }
    }

    private void MoveAndRotate()
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

        if (!isDay)
        {
            if (inputX > 0 && playerThrow.CurrentState != PlayerThrow.ThrowStates.Disabled) playerThrow.CurrentState = PlayerThrow.ThrowStates.Right;
            if (inputX < 0 && playerThrow.CurrentState != PlayerThrow.ThrowStates.Disabled) playerThrow.CurrentState = PlayerThrow.ThrowStates.Left;
        }

        if (directionVector.magnitude > Mathf.Abs(0.1f))
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotationVector), Time.deltaTime * 10);

        playerRB.velocity = directionVector * speed;

<<<<<<< HEAD
        if (inputX != 0)
        {
            playerAnim.speed = 1;
            playerAnim.Play("Move");
            PlayStepsSound(); // пїЅпїЅпїЅпїЅ <-- РЁРѕ СЌС‚Рѕ?  ento russciy yzic
        }
        else
        {
            playerAnim.speed = 0;
            playerAnim.Play("Stay");
        }

        /*if (useStair)
        {
            if (inputY != 0) PlayLadderSound(); // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ..  <-- Р СЌС‚Рѕ??? i ento toze
=======
        if (inputX != 0) PlayStepsSound(); // шаги

        /*if (useStair)
        {
            if (inputY != 0) PlayLadderSound(); // лестница лучше не включать..
>>>>>>> parent of 989d8c8 (Merge branch 'main' of https://github.com/shmyakkk/SiberianGameJam)
            else audioSource.Stop();
        }*/
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
        if (other.CompareTag("Ground"))
        {
            useStair = true;
            playerThrow.CurrentState = PlayerThrow.ThrowStates.Disabled;
        }

        if (other.CompareTag("Stair"))
        {
            isStair = false;
            playerRB.useGravity = true;
        }
    }
}
