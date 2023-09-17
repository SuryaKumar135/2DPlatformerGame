using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    bool isChest, isEnemy;
    float objectForceChest=550f;
    float objectForceItem = 50f;

    [SerializeField]
    GameObject dropGameObject;

    public bool itemDropped;
    public void Throwables()
    {
        Vector3 Temp = transform.position;
        Temp.y += 3;

        if (itemDropped)
        {
            return;
        }
        if(isChest)
        {
            GameObject obj=Instantiate(dropGameObject, transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * objectForceChest);

        }
        if (isEnemy)
        {
            //GameObject obj = Instantiate(dropGameObject, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
            GameObject obj = Instantiate(dropGameObject, Temp, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * objectForceItem);

        }
    }

    void Dropitem()
    {

    }
}
