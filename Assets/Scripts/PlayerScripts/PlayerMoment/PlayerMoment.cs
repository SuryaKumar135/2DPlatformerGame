using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoment : MonoBehaviour
{
    //player components
    Rigidbody2D rigidbody2DPlayer;
    PlayerAnimController playerAnimController;
    HealthScript REFhealthScript;
    //end components
    //Layer Masks
    public LayerMask groundMask;
    //Layer end


    [Header("player Movement and Jump")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float jumpForce;
    float xInput;
    bool groundCheck;
    public float groundCheckDistance = 5f;
    Vector2 playerDir;
    private void Awake()
    {
        rigidbody2DPlayer = GetComponent<Rigidbody2D>();
        playerAnimController = GetComponentInChildren<PlayerAnimController>();
        REFhealthScript=GetComponent<HealthScript>();
    }
    private void Update()
    {
        //Health
        REFhealthScript.healthImage.fillAmount = REFhealthScript.Health/100f;
        playerRun();
        rigidbody2DPlayer.velocity = new Vector2(playerDir.x,rigidbody2DPlayer.velocity.y);

        print(rigidbody2DPlayer.velocity.y);
       
    }
    void playerRun()
    {
        xInput = Input.GetAxis("Horizontal");
        playerDir.x = xInput * moveSpeed;
        if (xInput < 0 && isFacingRight)
        {
            Flip();
        }
        if (xInput > 0 && !isFacingRight)
        {
            Flip();
        }
        //animation logic
        if(rigidbody2DPlayer.velocity.x!=0)
        {
            playerAnimController.PlayerRun(true);
            playerAnimController.PlayerIdle(false);
        }
        else
        {
            playerAnimController.PlayerRun(false);
            playerAnimController.PlayerIdle(true);
        }
        PlayerJump();

    }
    bool isFacingRight = true;
    void Flip()
    {
        isFacingRight=!isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    void PlayerJump()
    {
        groundCheck=Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,groundMask);//raycast ot chaeck the ground detection
        if(Input.GetKeyDown(KeyCode.Space) && groundCheck)//on button click down
        {

            rigidbody2DPlayer.velocity = new Vector2(playerDir.x, jumpForce);//the veclocty is change to upwards
            
            playerAnimController.PlayerJump(true);//playe the jump animation
            playerAnimController.PlayerRun(false);//new change
            return;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimController.PlayerJump(false);
            rigidbody2DPlayer.velocity = new Vector2(playerDir.x, rigidbody2DPlayer.velocity.y);
        }


        //to playe the falling animations
        if (rigidbody2DPlayer.velocity.y != 0)
        {
            playerAnimController.PlayerFall(true);
        }
        if (rigidbody2DPlayer.velocity.y == 0 || rigidbody2DPlayer.velocity.y>-1)
        {
            playerAnimController.PlayerIdle(true);
            playerAnimController.PlayerFall(false);
        }
        PlayerClimb();
    }

    bool isclimbing,canClimb;
    float climbSpeed = 5;
    float yInput;
    void PlayerClimb()
    {
        yInput = Input.GetAxis("Vertical");

        if(canClimb&& math.abs(yInput)!=0)
        {
            isclimbing = true;
           
        }
       
        if(isclimbing)
        {
            rigidbody2DPlayer.gravityScale = 0f;
            rigidbody2DPlayer.velocity=new Vector2(rigidbody2DPlayer.velocity.x,yInput*climbSpeed);
            playerAnimController.PlayerIdle(!canClimb);
            playerAnimController.PlayerClimb((int)yInput);
        }
        else
        {
            rigidbody2DPlayer.gravityScale = 1f;
        }
           
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HealthPacks" && transform.GetComponent<HealthScript>().Health<100)
        {
            print("Health Recorved");
            REFhealthScript.healthImage.fillAmount += 5;
            REFhealthScript.Health += 5;
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.CompareTag("Ladder"))
        { 
                canClimb = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        
            if (collision.gameObject.CompareTag("Ladder"))
            {
                isclimbing = false;
                canClimb = false;
            }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("OutOffBounds"))
        {
            REFhealthScript.ApplyDamage(100);
            transform.GetComponent<KnockBack>().KnockBackEffect(Vector2.up);
        }
        if(collision.gameObject.CompareTag("Chest"))
        {
            collision.gameObject.GetComponent<Pickable>().Throwables();
            collision.gameObject.GetComponent<Pickable>().itemDropped = true;
        }
    }

  

}
