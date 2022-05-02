using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Transform tr;

    Rigidbody rb;

    [SerializeField]private GameObject Player;

    [SerializeField] private bool ToTheRight;

    [SerializeField] private float speed,Patrol_distans,DistanseOfVision, chanseClimbing;

    private bool climbing = false, already = false;

    [SerializeField]private bool harassment = false;

    private Vector3 upLadder, downLadder, ladderPos, StartPosition;

    int dir = 1, up = 8;

    float StartSpeed;

    float chanse = 0;

    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();

        StartPosition = tr.position;

        if (ToTheRight){
            dir = 1;
        }
        else{
            dir = -1;
        }
    }

    
    void FixedUpdate()
    {
        Enemy_move();
    }

    void Enemy_move(){

        if (!harassment && !climbing){
            rb.velocity = new Vector3(speed*dir, 0, 0);

            if (tr.position.x >= Patrol_distans + StartPosition.x){
                dir = -1;
            }
            if (tr.position.x <= StartPosition.x - Patrol_distans){
                dir = 1;
            }

        }

        var S = Player.transform.position - tr.position;
        if(S.magnitude < DistanseOfVision)
        {
            harassment = true;

            tr.position = Vector3.MoveTowards(tr.position, Player.transform.position, speed * Time.fixedDeltaTime);
        }
        else{
            harassment = false;
        }

    }

    void LadderMove(){
        rb.isKinematic = true;
        rb.velocity = new Vector3(rb.position.x, up * speed,0);
    }
      
    void OnTriggerStay (Collider  other)
	{
        if(other.gameObject.CompareTag("ladder") && !harassment)
        {
            if (!already){
                chanse = Random.Range(0,100);
                already = true;
            } 
            if (chanse <= chanseClimbing ){
                //rb.isKinematic = true; 
                ladderPos = other.transform.position;
                if (up == 8){
                    if(tr.position.y < ladderPos.y && !climbing)
                        up = 1;
                    else
                        up = -1;
                }
                climbing = true;
                rb.velocity = new Vector3(0,speed*up, 0);
                if (tr.position.x != ladderPos.x)
                    tr.position = Vector3.MoveTowards(tr.position, new Vector3(ladderPos.x, tr.position.y, tr.position.z), speed * Time.fixedDeltaTime);                
            }
        } 
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Ground")){
            if(up == -1){
                //rb.isKinematic = false;
                climbing = false;
                StartPosition = tr.position;
                ToTheRight = true;
                chanse = 101;
                Enemy_move();
                StartCoroutine(Waiting(2));
            }
        }
    }
    
    void OnTriggerExit (Collider  other)
	{
        if(other.gameObject.CompareTag("ladder"))
        {
            Debug.Log("Loh");
            //rb.isKinematic = false;
            climbing = false;
            StartPosition = tr.position;
            StartCoroutine(Waiting(2));
            Enemy_move();   

        }
    }

    IEnumerator Waiting(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rb.isKinematic = false;
        already = false;
        up = 8;

    }    
}