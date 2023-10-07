using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthScript : MonoBehaviour
{
    public UnityEngine.UI.Image healthImage;
    [SerializeField]
    private int health=100;
    [SerializeField]
    float deathTime=3f;

    public int Health
    {
        set
        {
            health = value;
            if (health <= 0 || health > 100)
            {
                return;
            }
        }
        get { return health; }
    }

    bool isDead;

    [SerializeField]
    bool isPlayer;
    [SerializeField]
    bool isEnemy;

    public void ApplyDamage(int damage)
    {
        Health-=damage;
        if (Health <= 0)
        {
            isDead = true;
            Health = 0;
        }
        CharacterIsDead();
    }
    public void CharacterIsDead()
    {
        if (isDead)
        {
            if (isPlayer)
            {
                transform.GetComponent<PlayerMoment>().enabled = false;
                transform.GetComponentInChildren<PlayerAnimController>().PlayerDeath();
                transform.GetComponent<Collider2D>().enabled = false;
                transform.GetComponent<KnockBack>().enabled = false;
                transform.GetComponentInChildren<PlayerAnimController>().enabled = false;
            }
            if(isEnemy)
            {
                transform.GetComponent<Pickable>().Throwables();
                transform.GetComponent<Pickable>().itemDropped = true;
                //enemy drop end
                transform.GetComponent<EnemyMomentScript>().enabled = false;

                transform.GetComponent<EnemyAnimator>().EnemyDeath();
                transform.GetComponent<Collider2D>().enabled = false;
            }
            Invoke("SetActive", deathTime);
        }
    }

    void SetActive()
    {
        gameObject.SetActive(false);
    }
}
