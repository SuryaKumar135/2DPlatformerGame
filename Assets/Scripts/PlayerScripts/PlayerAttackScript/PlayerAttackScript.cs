using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public bool canRaciveInput;
    public bool inputRecived;
    public static PlayerAttackScript instance;

    //attack detection variables
    public float attackRadius;
    public Transform attackPoint;
    private Collider2D attackCollider;

    //attack strength power
    [SerializeField]
    int attackPower;

    //component 
   
   
    private void Awake()
    {
       
        

          instance = this;
        
       
    }
    private void Update()
    {
       
        Attack();
      
    }
    public void Attack()
    {
        if((Input.GetMouseButtonDown(0)))
        {

           
               
            if (canRaciveInput)
            {
                
                inputRecived = true;
                canRaciveInput = false;
            }
            else
            {
               
                return;
            }
        }
    }
    public void InputTrigger()
    {
        if(!canRaciveInput)
        {
           

            canRaciveInput = true;
        }
        else
        {
            
            canRaciveInput = false;
        }
    }
    private void attackDetection( bool canDetect)
    {
        if (canDetect)
        {


            attackCollider = Physics2D.OverlapCircle(attackPoint.position, attackRadius, LayerMask.GetMask("Enemy"));

            if (attackCollider != null)
            {
                attackCollider.transform.GetComponent<HealthScript>().ApplyDamage(attackPower);
                attackCollider.transform.GetComponent<KnockBack>().KnockBackEffect(transform.gameObject);
               
            }
        }
      
    }
    public void turnOnAttackPoint()
    {
        attackDetection(true);
    }
    public void turnOffAttackPoint()
    {
        attackDetection(false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
