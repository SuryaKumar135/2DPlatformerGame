using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator enemyAnimator;
    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }
    public void enemyIdle(bool idle)
    {
        enemyAnimator.SetBool("Idle", idle);
    }
    public void enemyPatrol(bool walk)
    {
        enemyAnimator.SetBool("Patrol", walk);
    }
    public void enemyChase(bool chase)
    {
        enemyAnimator.SetBool("Chase", chase);
    }
    public void enemyAttack()
    {
        enemyAnimator.SetTrigger("Attack");
    }
    public void enemyDeath()
    {
        enemyAnimator.SetTrigger("Death");
    }
}
