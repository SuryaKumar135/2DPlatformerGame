using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum EnemyState
{
    Patrol,
    Chase,
    Attack
}


public class EnemyMomentScript : MonoBehaviour
{
    //-----------------------------ENEMY PATROL VARIABLES-----------------------------\\
    [Header("Enemy Patrol")]
    [SerializeField]
    float enemySpeed;//enemy patrol speed 

    [SerializeField]
    float enemyGroundDetectionDistance;//size fo ray which check ground

    [SerializeField]
    float enemyWallDetectionDistance;//size fo ray which check wall

    [SerializeField]
    LayerMask platformLayerMask;

    [SerializeField]
    private Transform enemyRayDetectionArea;//transformchild for wall and ground detection

    //-----------------------------ENEMY DETECT VARIABLES-----------------------------\\
    [Header("Enemy Detect")]
    [SerializeField]
    Transform enemyDetectionTransform;
    [SerializeField]
    float boxSizeX;
    [SerializeField]
    float boxSizeY;
    [SerializeField]
    LayerMask playerLayerMask;
    RaycastHit2D wallTOPlayerDetection;
    Collider2D boxDetection;

    //start initialize variables
    EnemyState enemyState;
    Rigidbody2D enemyRigidbodySD;
    EnemyAnimator enemyAnimator;

    //start initialize variables
    void Start()
    {
        enemyState = EnemyState.Patrol;
        enemyRigidbodySD = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        currChaseSpeed = chaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        groundInfo = Physics2D.Raycast(enemyRayDetectionArea.position, Vector2.down, enemyGroundDetectionDistance);
        wallInfo = Physics2D.Raycast(enemyRayDetectionArea.position, enemyRayDetectionArea.transform.right, enemyWallDetectionDistance, platformLayerMask);
        if (enemyState == EnemyState.Patrol)
        {
            EnemyPatrol();

        }
        if (enemyState == EnemyState.Chase)
        {
            EnemyChase();

        }
        if (enemyState == EnemyState.Attack)
        {
            EnemyAttack();
        }
       
        EnemyDetect();
    }

    RaycastHit2D wallInfo;
    RaycastHit2D groundInfo;
    public void EnemyPatrol()
    {

        enemyAnimator.EnemyIdle(false);
        enemyAnimator.EnemyPatrol(true);
       
        Debug.DrawRay(enemyRayDetectionArea.position, Vector2.down * enemyGroundDetectionDistance, Color.blue);
        Debug.DrawRay(enemyRayDetectionArea.position, new Vector2(enemyWallDetectionDistance, 0) * enemyWallDetectionDistance, Color.green);

        transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);

        //ground Detection
        if (groundInfo.collider == null || wallInfo.collider != null)
        {

            Flip();
        }

    }
    bool playerDetected;
    void EnemyDetect()
    {
        boxDetection = Physics2D.OverlapBox(enemyDetectionTransform.position, new Vector2(boxSizeX, boxSizeY), 0, playerLayerMask);
        if (boxDetection != null)
        {
            wallTOPlayerDetection = Physics2D.Raycast(enemyDetectionTransform.position, boxDetection.transform.position - enemyDetectionTransform.position, Vector2.Distance(enemyDetectionTransform.position, boxDetection.transform.position), platformLayerMask);
            Debug.DrawRay(enemyDetectionTransform.position, boxDetection.transform.position - enemyDetectionTransform.position, Color.black);

            if (wallTOPlayerDetection.collider == null)
            {
                enemyState = EnemyState.Chase;
                playerDetected = true;

            }
            else
            {
                enemyState = EnemyState.Patrol;
                playerDetected = false;
                return;

            }
        }
        else
        {
            playerDetected = false;
            enemyState = EnemyState.Patrol;
        }
    }
    //Enemy Detection 
    public bool isFacingRight = true;
    void Flip()
    {
        isFacingRight = !isFacingRight;
        if (isFacingRight)
        {
            transform.Rotate(0, 0, 0);
        }
        else
        {

            transform.Rotate(0, 180, 0);
        }
    }
    [Header("Enemy Chase")]
    //chase Variables
    [SerializeField]
    float chaseSpeed;
    [SerializeField]
    float attackDistance;

    float currChaseSpeed;
    void EnemyChase()
    {
        if (boxDetection != null && playerDetected)
        {
            //Attack Code
            if (Vector2.Distance(enemyDetectionTransform.position, boxDetection.transform.position) <= attackDistance)
            {
                enemyRigidbodySD.velocity=Vector2.zero;
                enemyState = EnemyState.Attack;
               
                return;
            }
            else
            {
               
                enemyAnimator.EnemyChase(true);
                enemyAnimator.EnemyPatrol(false);
               
            }
            //Attack code End

            //Enemy to not jump of the map
            if (groundInfo.collider == null || wallInfo.collider != null)
            {
                print("Cant Jump Off");
                enemyRigidbodySD.velocity = Vector2.zero;
                enemyAnimator.EnemyIdle(true);
                enemyAnimator.EnemyChase(false);
                return;
            }
            else
            {
              //  print("Cant Jump Off");
                enemyState = EnemyState.Chase;
                enemyAnimator.EnemyChase(true);
                enemyAnimator.EnemyIdle(false);
            }
            ChaseDirection();

           
            
        }
    }
    void ChaseDirection()
    {
        if (boxDetection.gameObject.transform.position.x > transform.position.x)
        {

            enemyAnimator.EnemyChase(true);
            enemyAnimator.EnemyPatrol(false);
            enemyRigidbodySD.velocity = new Vector2(chaseSpeed, enemyRigidbodySD.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {

            enemyAnimator.EnemyChase(true);
            enemyAnimator.EnemyPatrol(false);
            enemyRigidbodySD.velocity = new Vector2(-chaseSpeed, enemyRigidbodySD.velocity.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    void EnemyAttack()
    {
        enemyAnimator.EnemyAttack();
        enemyAnimator.EnemyPatrol(false);
        enemyAnimator.EnemyChase(false);
    }
    //variables for enemy damage and attack detection
    [Header("Enemy Attack")]
    Collider2D attackDetection;
    [SerializeField]
    Transform attackPosition;
    [SerializeField]
    float attackRadius;
    [SerializeField]
    int enemyDamage;
    void EnemyAttackDamage(bool canDetect)
    {
        if (canDetect)
        {
            attackDetection = Physics2D.OverlapCircle(attackPosition.position, attackRadius, playerLayerMask);
            if (attackDetection != null)
            {
                attackDetection.gameObject.GetComponent<HealthScript>().ApplyDamage(enemyDamage);
                attackDetection.gameObject.GetComponent<KnockBack>().KnockBackEffect(transform.gameObject);
            }
        }
        else
        {
            return;
        }
    }
    public void turnOnAttackPoint()
    {
        EnemyAttackDamage(true);
    }
    public void turnOffAttackPoint()
    {
        EnemyAttackDamage(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(enemyDetectionTransform.position, new Vector2(boxSizeX, boxSizeY));
        Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }

  
}

