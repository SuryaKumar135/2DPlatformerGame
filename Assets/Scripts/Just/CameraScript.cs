using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float boundX = 0.15f;
    public float boundY = 0.05f;
    public Transform lookAt;//camera ponit of view
    public GameObject LeveFailScreen;
   
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        float deltaX = lookAt.position.x - transform.position.x;//player position
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)

            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;

            }
        }


        //for y axis of camera momment
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)

            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;

            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
  
  

}
