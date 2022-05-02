using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private ParticleSystem bangPS;
    private Rigidbody bombRB;
    private Vector3 throwDirection;
    private float force;

    public float Force { get => force; set => force = value; } 

    private void Start()
    {
        bombRB = GetComponent<Rigidbody>();
        throwDirection = new Vector3(1, 1, 0);

        bombRB.AddForce(throwDirection * Force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var bang = Instantiate(bangPS, gameObject.transform.position, bangPS.transform.rotation);
        bang.Play();

        EnemyController.BombCoord = transform.position;

        Destroy(gameObject);
    }
}
