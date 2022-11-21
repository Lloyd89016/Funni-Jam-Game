using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 1;
   [SerializeField] float cooldown = 1f;

    private void Start()
    {
        cooldown = 0;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(cooldown <= 0)
        {
            if (collision.gameObject.tag == ("Player"))
            {
                playerHealth.TakeDamage(damage);
            }
        }

     
    }
    private void Update()
    {
        cooldown -= .1f * Time.deltaTime;
    }
}
