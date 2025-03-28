using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{

    public float spawnRadius = 20f;
    public bool clockWise;
    public float timer;
    public float timerRate;
    public GameObject slices;
    void Update(){
        pizzaSlices();
    }

    public void pizzaSlices(){
        timer -= Time.deltaTime;
        if(timer <0){
            Vector2 delta = Random.insideUnitCircle * spawnRadius;
            Vector3 pos = new Vector3(delta.x, 0 ,delta.y);
            float randomY = Random.Range(0.0f, 360.0f);
            Quaternion newQuaternion = new Quaternion();
            newQuaternion.Set(0,randomY,0,360.0f); 
            Instantiate(slices, pos,newQuaternion);
            timer = 1.0f/timerRate;

        }  
    }
}
