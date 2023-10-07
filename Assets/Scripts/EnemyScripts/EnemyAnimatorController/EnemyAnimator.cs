using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator enemyAnimator;

    int hashId_Idle = Animator.StringToHash("Idle");
    int hashId_Patrol = Animator.StringToHash("Patrol");
    int hashId_Chase = Animator.StringToHash("Chase");
    int hashId_Attack = Animator.StringToHash("Attack");
    int hashId_Death = Animator.StringToHash("Death");
    int hashId_Hurt = Animator.StringToHash("Hurt");
    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }
    public void EnemyIdle(bool idle)
    {
        enemyAnimator.SetBool(hashId_Idle, idle);
    }
    public void EnemyPatrol(bool patrol)
    {
        enemyAnimator.SetBool(hashId_Patrol, patrol);
    }
    public void EnemyChase(bool chase)
    {
        enemyAnimator.SetBool(hashId_Chase, chase);
    }
    public void EnemyAttack()
    {
        enemyAnimator.SetTrigger(hashId_Attack);
    }
    public void EnemyDeath()
    {
        enemyAnimator.SetTrigger(hashId_Death);
    }
    public void EnemyHurt()
    {
        enemyAnimator.SetTrigger(hashId_Hurt);
    }
}
