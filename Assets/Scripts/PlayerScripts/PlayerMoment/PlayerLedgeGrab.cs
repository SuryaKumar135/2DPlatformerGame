using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeGrab : MonoBehaviour
{
    [SerializeField]
    float radiusLedgeDetect;
    PlayerMoment playerMoment_REF;
    bool canClimb;
    // Start is called before the first frame update
    void Start()
    {
       
          playerMoment_REF = GetComponentInParent<PlayerMoment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canClimb)
            playerMoment_REF.ledgeGrabDetection = Physics2D.OverlapCircle(transform.position, radiusLedgeDetect, LayerMask.GetMask("Ground"));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            canClimb = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canClimb = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, radiusLedgeDetect);
    }
}
