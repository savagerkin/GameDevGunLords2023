using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public GameObject deathEffect;
    public HealthBar healthBar;
    public ThunderLord thunderLord;
    public void Start(){
        SceneLoad.f = 0;
        SceneLoad.canSwitch = true;

        healthBar.SetMaxHealth(health);
    }
    public void takeDamage(int damage){
        health -= damage;
        if(healthBar != null){healthBar.SetHealth(health);}
        if(thunderLord != null){thunderLord.increaseFireRate(damage);}
        if(health <= 0){
            if(deathEffect != null){
                PlayerHealth.canLose = false;
                Instantiate(deathEffect, transform.position + 5 * Vector3.down, Quaternion.Euler(-90,0,0));
                SceneLoad.f = 1;
            }
            Destroy(gameObject);
        }
    }
}
