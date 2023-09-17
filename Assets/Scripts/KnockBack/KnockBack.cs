using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
   
    Rigidbody2D rb2DREf;

    Vector3 dir;
    public float strength = 300f,delay=0.15f;
    private void Awake()
    {
        rb2DREf = GetComponent<Rigidbody2D>();
    }
    public void KnockBackEffect(GameObject sender)
    {
        
         StartCoroutine(Reset());
         dir=(transform.position-sender.transform.position);
        
         rb2DREf.AddForce(dir*strength,ForceMode2D.Impulse);
        
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
