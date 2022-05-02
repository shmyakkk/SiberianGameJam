using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem bangPS;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bombSound;

    private Rigidbody bombRB;
    private Vector3 throwDirection;
    private float force;

    public float Force { get => force; set => force = value; }
    public Vector3 ThrowDirection { get => throwDirection; set => throwDirection = value; }

    private void Start()
    {
        bombRB = GetComponent<Rigidbody>();

        bombRB.AddForce(ThrowDirection * Force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var bang = Instantiate(bangPS, gameObject.transform.position, bangPS.transform.rotation);
        bang.Play();

        audioSource.PlayOneShot(bombSound);
        EnemyController.BombCoord = transform.position;
        Destroy(gameObject);
    }
}
