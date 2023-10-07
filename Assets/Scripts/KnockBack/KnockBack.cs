using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
   
    Rigidbody2D rb2DREf;

    Vector3 dir;
    public float strength = 300f,delay=0.15f;

    public bool isPlayer, IsEnemy;

    private PlayerAnimController playerAnimController;
    private EnemyAnimator enemyAnimator;
    private void Awake()
    {
        rb2DREf = GetComponent<Rigidbody2D>();
        playerAnimController=GetComponentInChildren<PlayerAnimController>();
        enemyAnimator=GetComponent<EnemyAnimator>();
    }
    public void KnockBackEffect(GameObject sender)
    {
        
         StartCoroutine(Reset());
         dir.x=(transform.position.x-sender.transform.position.x);
        
         rb2DREf.AddForce(dir*strength,ForceMode2D.Impulse);
        if(isPlayer)
        {
            playerAnimController.PlayerHurt();
        }
        //if(IsEnemy)
        //{
        //    enemyAnimator.EnemyHurt();
        //}
        
    }
    public void KnockBackEffect(Vector2 dir)
    {

        //StartCoroutine(Reset());
        rb2DREf.AddForce(dir * strength, ForceMode2D.Impulse);

    }
    IEnumerator Reset()
    {
        print("before");
        rb2DREf.velocity = Vector3.zero;
        yield return new WaitForSeconds(delay);
        print("Wait");


    }


}
