using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 50f;
    public HealthBar healthBar;

    public static bool canLose = true;
    public void Start(){
        healthBar.SetMaxHealth(health);
    }
    public void takeDamage(int damage){
        health -= damage;
        healthBar.SetHealth(health);
        if(health <= 0 && canLose){
            SceneManager.LoadScene (5);
        }
    }
}
