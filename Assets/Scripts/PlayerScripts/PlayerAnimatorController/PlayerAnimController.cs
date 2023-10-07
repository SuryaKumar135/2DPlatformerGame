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
    int hashId_Idle = Animator.StringToHash("Idle");
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
        PlayerAnim.SetBool("WallSlide", slide);

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
    public void PlayerHurt()
    {
        PlayerAnim.SetTrigger("Hurt");
    }
    public void PlayerLedgeClimb(bool Ledge)
    {
        PlayerAnim.SetBool("LedgeClimb", Ledge);
    }
    public void PlayerLadderClimb(bool LadderClimb)
    {
        PlayerAnim.SetBool("LadderClimb", LadderClimb);
    }
}//class
