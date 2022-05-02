using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    [SerializeField]private GameObject Player;
    [SerializeField] private bool ToTheRight;
    [SerializeField] private float speed,Patrol_distans,VisionFace,VisionBack,VertVision, chanseClimbing;
    private bool climbing = false, already = false, harassment = false;
    private Vector3 upLadder, downLadder, ladderPos, StartPosition;
    private int dir = 1, up = 8;
    [SerializeField]private float rotationSpeed;
    public static Vector3 BombCoord;
    private float chanse = 0;
    private Vector3 rotationVector;
    private bool Boom;

    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();

        StartPosition = tr.position;
        if (ToTheRight){
            dir = 1;
            tr.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,-90, transform.rotation.eulerAngles.z);
        }
        else{
            dir = -1;
            tr.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,90, transform.rotation.eulerAngles.z);
        }

        BombCoord.z = -1000;
    }
    void FixedUpdate()
    {
        Enemy_move();
        if (BombCoord.z != -1000){
            Debug.Log(BombCoord);

            Boom_go();
        }
    }
    void Boom_go(){
        var d = BombCoord - tr.position;
        if (!harassment)
            if (d.magnitude < VisionFace && Mathf.Abs(BombCoord.y - tr.position.y) <= VertVision){
                if (tr.rotation.eulerAngles.y == 90 && (BombCoord.x < tr.position.x)){
                    Boom = true;
                } else if(BombCoord.x > tr.position.x && tr.position.x - BombCoord.x <= VisionBack){
                    tr.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,-90, transform.rotation.eulerAngles.z);
                    Boom = true;
                } else if(tr.rotation.eulerAngles.y == -90 && (BombCoord.x > tr.position.x)){
                        Boom = true;
                    }else if (BombCoord.x < tr.position.x && BombCoord.x - tr.position.x <= VisionBack){
                        tr.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,90, transform.rotation.eulerAngles.z);
                        Boom = true;
                    }
                if (Boom){
                    tr.position = Vector3.MoveTowards(tr.position, BombCoord, speed * Time.fixedDeltaTime);
                }
            }
            if (Mathf.Abs(tr.position.x - BombCoord.x) <= 0.05f){
                StartCoroutine(Look_at_Boom(2));
            }
    }
    IEnumerator Look_at_Boom(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Boom = false;
        BombCoord.z = -1000;
    }
    void Enemy_move(){
        if (!harassment && !climbing && !Boom){
            
                rb.velocity = new Vector3(speed*dir, 0, 0);

            if (tr.position.x >= Patrol_distans + StartPosition.x){
                dir = -1;
                tr.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,90, transform.rotation.eulerAngles.z);
            }
            if (tr.position.x <= StartPosition.x - Patrol_distans){
                dir = 1;
               tr.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,-90, transform.rotation.eulerAngles.z);
            }
        }
        var S = Player.transform.position - tr.position;
        if(S.magnitude < VisionFace && Mathf.Abs(Player.transform.position.y - tr.position.y) <= VertVision)
        {
            if (tr.rotation.eulerAngles.y == 90 && (Player.transform.position.x < tr.position.x)){
                harassment = true;
            } else if(Player.transform.position.x > tr.position.x && tr.position.x - Player.transform.position.x <= VisionBack){
                tr.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,-90, transform.rotation.eulerAngles.z);
                harassment = true;
            } else if(tr.rotation.eulerAngles.y == -90 && (Player.transform.position.x > tr.position.x)){
                    harassment = true;
                }else if (Player.transform.position.x < tr.position.x && Player.transform.position.x - tr.position.x <= VisionBack){
                    tr.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,90, transform.rotation.eulerAngles.z);
                    harassment = true;
                }
            if (harassment)
                tr.position = Vector3.MoveTowards(tr.position, Player.transform.position, speed * Time.fixedDeltaTime);
        }
        else{
            harassment = false;
        }

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