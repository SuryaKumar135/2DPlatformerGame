using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    //player climb for player
    bool isclimbing, canClimb;
    float climbSpeed = 5;
    float yInput;
    //end Climb


    //Player Ledge Grab
    [HideInInspector]
    public  bool ledgeGrabDetection;//Collider

    [SerializeField]
    Vector2 offSet1;

    [SerializeField]
    Vector2 offSet2;

    [SerializeField]
    Vector2 offSet3;

    [SerializeField]
    Vector2 offSet4;

    Vector2 climbBeginPosition,climbOverPosition;

    bool canGrabLedge=true, canLedgeClimb;


    //end lGrab

    //PlayerWallSlide
    [Header("Wall Slide")]
    [SerializeField]
    Transform wallCheckPosition;
    [SerializeField]
    Vector2 boxSize;
    [SerializeField]
    LayerMask maskToWallSlideCheck;

    public float slidingSpeed;

    bool wallSlideDetection;//boxcollider
    bool canWallSlide;
    bool checWallSlide;
    //WallSlideEnd
    private void Awake()
    {
        rigidbody2DPlayer = GetComponent<Rigidbody2D>();
        playerAnimController = GetComponentInChildren<PlayerAnimController>();
        REFhealthScript=GetComponent<HealthScript>();
       
    }
    private void Update()
    {
        yInput = Input.GetAxis("Vertical");
        //Health
       // REFhealthScript.healthImage.fillAmount = REFhealthScript.Health/100f;
        playerRun();
        PlayerLedgeGrab();
        rigidbody2DPlayer.velocity = new Vector2(playerDir.x,rigidbody2DPlayer.velocity.y);

        // print(rigidbody2DPlayer.velocity.y);
        wallSlideDetection = Physics2D.OverlapBox(wallCheckPosition.position, boxSize, 0, maskToWallSlideCheck);

        if(rigidbody2DPlayer.velocity.x==0)
        {
            playerDir.x = 0;
            print(rigidbody2DPlayer.velocity.x);
            playerAnimController.PlayerIdle(true);
        }

        CheckFlip();
    }

    /// <summary>
    /// Player Movement Fuction
    /// </summary>
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
    bool canFlip = true;
    void Flip()
    {
        if (canFlip)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }
    void CheckFlip()
    {
        if(WallSlideCheck() || canLedgeClimb || isclimbing)
        {
            canFlip = false;
        }
        else
        {
            canFlip = true;
        }
    }
    void PlayerJump()
    {
      
        groundCheck =Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,groundMask);//raycast ot chaeck the ground detection
        if(Input.GetKeyDown(KeyCode.Space) && groundCheck )//on button click down
        {

            rigidbody2DPlayer.velocity = new Vector2(playerDir.x, jumpForce);//the veclocty is change to upwards
            Invoke("PlayerJumpCancle", 0.5f);
           
            playerAnimController.PlayerJump(true);//player the jump animation
            playerAnimController.PlayerRun(false);//new change
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            playerAnimController.PlayerJump(false);
            rigidbody2DPlayer.velocity = new Vector2(playerDir.x, rigidbody2DPlayer.velocity.y);
        }


        //to playe the falling animations
        if (rigidbody2DPlayer.velocity.y != 0 && !isclimbing)
        {
            playerAnimController.PlayerFall(true);
            
        }
        if (rigidbody2DPlayer.velocity.y == 0 || rigidbody2DPlayer.velocity.y>-1)
        {
           // playerAnimController.PlayerIdle(true);
            playerAnimController.PlayerFall(false);
        }
        PlayerClimb();
    }
    void PlayerJumpCancle()
    {
        playerAnimController.PlayerJump(false);
        rigidbody2DPlayer.velocity = new Vector2(playerDir.x, rigidbody2DPlayer.velocity.y);
    }
    void PlayerLedgeGrab()
    {
        
        if(ledgeGrabDetection && canGrabLedge)
        {
            canGrabLedge = false;
            Vector2 localPosition = GetComponentInChildren<PlayerLedgeGrab>().transform.position;

            if (isFacingRight)
            {
                // Right side grab
                climbBeginPosition = localPosition + offSet1;
                climbOverPosition = localPosition + offSet2;
            }
            else
            {
                // Left side grab
                climbBeginPosition =localPosition + offSet3;  // Adjust the offset for left side
                climbOverPosition = localPosition + offSet4;  // Adjust the offset for left side
            }

            canLedgeClimb = true;
        }
        if (canLedgeClimb)
        {
            transform.position = Vector2.Lerp(transform.position, climbBeginPosition, 1); ;
        }
        playerAnimController.PlayerLedgeClimb(canLedgeClimb);
        
    }
    public void LedgeClimbOver()
    {
        transform.position = Vector2.Lerp(climbBeginPosition, climbOverPosition,1);
        canLedgeClimb =false;
        Invoke("AllowLedgeGrab", 1f);
    }
    private void AllowLedgeGrab() => canGrabLedge = true;
    void PlayerClimb()//Ladder Climb
    {
       // print(yInput)
        if(canClimb && math.abs(yInput) != 0)
        {
            isclimbing = true;
          

        }
            playerAnimController.PlayerClimb((int)yInput);//Player Climb Animation
        if (isclimbing)
        {
           
            print("Ladder Climbing");
            rigidbody2DPlayer.velocity=new Vector2(rigidbody2DPlayer.velocity.x,yInput*climbSpeed);
            playerAnimController.PlayerIdle(!canClimb);
            playerAnimController.PlayerLadderClimb(true);
            rigidbody2DPlayer.gravityScale = 0f;
        }
        else
        {
            playerAnimController.PlayerLadderClimb(false);
            rigidbody2DPlayer.gravityScale = 1f;
        }

        PlayerWallSlide();


    }

    //Player Wall Slide
    void PlayerWallSlide()
    {
        if(WallSlideCheck())
        {
            rigidbody2DPlayer.velocity = new Vector2(rigidbody2DPlayer.velocity.x, Mathf.Clamp(rigidbody2DPlayer.velocity.y, -slidingSpeed, float.MaxValue));
            xInput = 0;

            playerAnimController.PlayerSlide(true);
        }
        else
        {
            playerAnimController.PlayerSlide(false);
        }
    }
    bool WallSlideCheck()
    {
        if(wallSlideDetection && !groundCheck)
        {
            canWallSlide = true;
        }
        else
        {
            canWallSlide = false;
        }
        return canWallSlide;
    }

    void WallJump()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        if (collision.gameObject.tag == "HealthPacks" && transform.GetComponent<HealthScript>().Health < 100)
        {
            print("Health Recorved");
            REFhealthScript.healthImage.fillAmount += 5;
            REFhealthScript.Health += 5;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("OutOffBounds"))
        {
            REFhealthScript.ApplyDamage(100);
           // transform.GetComponent<KnockBack>().KnockBackEffect(Vector2.up);
        }
        if(collision.gameObject.CompareTag("Chest"))
        {
            collision.gameObject.GetComponent<Pickable>().Throwables();
            collision.gameObject.GetComponent<Pickable>().itemDropped = true;
        }
       
            
    }
}
