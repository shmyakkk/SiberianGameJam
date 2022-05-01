using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Transform tr;

    Rigidbody rb;

    [SerializeField]private GameObject Player;

    [SerializeField] private bool StartToTheRight;

    [SerializeField] private float speed,Patrol_distans,DistanseOfVision, chanseClimbing;

    private bool harassment = false, climbing = false, already = false;

    private Vector3 upLadder, downLadder, ladderPos, StartPosition;

    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();

        StartPosition = tr.position;

        if (StartToTheRight){
            tr.Translate(new Vector3(speed,0,0) * Time.deltaTime);
            Debug.Log("1");
        }
        else{
            tr.Translate(new Vector3(-speed,0,0) * Time.deltaTime);
            Debug.Log("2");
        }
    }

    
    void FixedUpdate()
    {
        Enemy_move();
    }

    void Enemy_move(){
        if (!harassment && !climbing){

            if( tr.position.x > Patrol_distans + StartPosition.x)
            {
                Debug.Log("3");
                
                tr.Translate(new Vector3(speed,0,0) * Time.deltaTime);
            }
            else
                if(tr.position.x < StartPosition.x - Patrol_distans)
                {
                    Debug.Log("4");
                    tr.Translate(new Vector3(-speed,0,0) * Time.deltaTime);
                }
        }

        var S = Player.transform.position - tr.position;
        if(S.sqrMagnitude < DistanseOfVision*DistanseOfVision)
        {
            harassment = true;

            tr.LookAt(Player.transform);

            tr.position = Vector3.MoveTowards(tr.position, Player.transform.position, speed * Time.deltaTime);
        }
        else{
            harassment = false;
        }
    }

    void OnTriggerStay (Collider  other)
	{
        if(other.gameObject.CompareTag("ladder") && !already)
        {
           
            
            float chanse = Random.Range(0,100);
            already = true;

            if (chanse <= (chanseClimbing)){
                climbing = true; 
                Ladder ladder = other.GetComponent<Ladder>();
                upLadder = ladder.up.position;
                downLadder = ladder.down.position;
                ladderPos = other.transform.position;

                int up = -1;
                if(tr.position.y < upLadder.y+5)
		        {
			        up = 1;
		        }

                rb.isKinematic = true;
		        tr.Translate(new Vector2(0, speed * up * Time.fixedDeltaTime));
            }

        }
    }

    void OnTriggerExit (Collider  other)
	{
        if(other.gameObject.CompareTag("ladder"))
        {
            climbing = false;
            rb.isKinematic = false;
            already = false;
            tr.Translate(new Vector3(speed,0,0) * Time.deltaTime);
        }
    }
}