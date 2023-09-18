using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator PlayerAnim;
    
    private void Awake()
    {
        PlayerAnim=GetComponent<Animator>();   
    }
    public void PlayerIdle(bool idle)
    {
       PlayerAnim.SetBool("Idle",idle);
    }
    public void PlayerRun(bool run)
    {
        PlayerAnim.SetBool("Run",run);
    }
    public void PlayerJump(bool jump)
    {
        PlayerAnim.SetBool("Jump", jump);

    }
    public void PlayerSlide(bool slide)
    {
        PlayerAnim.SetBool("Sliding", slide);

    }
    public void PlayerFall(bool fall)
    {
        PlayerAnim.SetBool("Falling",fall);

    }
    public void PlayerDeath()
    {
        PlayerAnim.SetTrigger("Death");
    }
    public void PlayerClimb(int climb)
    {
        PlayerAnim.SetInteger("Climb",climb);
    }
}//class
