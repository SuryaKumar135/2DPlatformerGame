using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    // Start is called before the first frame update
    public KnockBack knockBack;
    public HealthScript healthScript;
    void Awake()
    {
        //Singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
              //Destroy - Removes a GameObject, component or asset.
                Destroy(instance);
        }
        //End

        //components needed

        
    }

    // Update is called once per frame
}
