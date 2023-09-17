using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   public Transform PlayerRef;
  
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (hitDetected.collider.gameObject != CompareTag("wall") && hitDetected.collider.gameObject.CompareTag("player"))
        //{
        //    print("ddetected");
        //    Debug.DrawRay(transform.position, direction * 10.0f, Color.green);

        Vector3 direction = (PlayerRef.position - transform.position).normalized;
        Collider2D RangeCollider2D = Physics2D.OverlapCircle(transform.position, 10.0f);
         RaycastHit2D hitDetected = Physics2D.Raycast(transform.position, direction, 20.0f);
      
        //  if (Rangehit.collider!=null)
        // {




        if (RangeCollider2D != null )
        {
           
            print("Player detected in range");
            if (hitDetected.collider != null)
            {
              

                if (hitDetected.collider.gameObject.CompareTag("Player"))
                {
                    print("detected");
                    Debug.DrawRay(transform.position, direction * 10.0f, Color.black);
                   
                }
            }
        }



    }
}
