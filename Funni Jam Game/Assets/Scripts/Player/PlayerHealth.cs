using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public HealthBarScript healthBar;
    public int maxHealth;

    void Start()
    {    
        maxHealth = health;
    }

    private void FixedUpdate()
    {
        healthBar.SetHealth(health);
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();  
        }

        // if health goes over maxhealth, set it to max health
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    void Die()
    {
        //Reloads the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    #region debugButton
    // debug button heal
    public void heal()
    {
        health += 10;
    }
    // debug button damage
    public void damage()
    {
        health -= 10;
    }
    #endregion
}
