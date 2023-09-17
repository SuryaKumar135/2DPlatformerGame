using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    float keyPressed=0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            keyPressed = Time.time;
            print(keyPressed);
            if(keyPressed > 3f)
            {
                print("!nope");
            }

        }
       
        
    }
}
