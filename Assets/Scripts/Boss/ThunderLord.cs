using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderLord : MonoBehaviour
{
    public float testX;
    public float testY;
    public float testZ;
    public float testQ;
    Transform player;
    public Transform[] gunEnds;
    public GameObject lowerHalf;

    public GameObject ball;
    public GameObject scaryCircle;
    public float spawnRadius = 10f;
    public float sliceRadius = 10f;

    public float spawnRate = 2;
    public float spawnTimer = -1;
    public float timer = 0;
    public float sliceTimer = -1;
    public float sliceRate = 2;

    public float waveTimer = -4;

    public float waveRate = 0.5f;
    public bool clockWise;
    public float timerRate;
    public GameObject slices;
    public float fireRate = 1;//bullets per second
    int random_number = 60;
    float increase_fire_rate;
    public GameObject waves;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //to make it more random
        int random_number = Random.Range(0, 100);
        transform.LookAt(player);
        spawBullets();
        if (true)
        {//random_number > 30
            spawnExplosions();
        }
        sliceTimer -= Time.deltaTime;
        if (true)
        {//random_number > 45)
            if (sliceTimer < 0)
            {
                spawnSlices();
            }
        }
        spawnTimer -= Time.deltaTime;
        spawnWave();
    }
    public void increaseFireRate(int damage)
    {
        float floatDamage = damage;
        spawnRate = spawnRate + floatDamage / 35;
        timerRate = timerRate + floatDamage / 360;
        fireRate = fireRate + floatDamage / 70;
    }
    void spawBullets()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            foreach (Transform gunEnd in gunEnds)
            {
                Instantiate(ball, gunEnd.position, gunEnd.rotation);
            }
            timer = 1 / fireRate;
        }
    }

    void spawnExplosions()
    {
        if (spawnTimer < 0)
        {
            //find postition
            Vector2 delta = Random.insideUnitCircle * spawnRadius;
            Vector3 pos = new Vector3(delta.x, 0, delta.y);
            //spawn explosion
            Instantiate(scaryCircle, pos, Quaternion.identity);
            spawnTimer = 1.0f / spawnRate;
        }
    }

    void spawnSlices()
    {
        sliceTimer -= Time.deltaTime;
        if (sliceTimer < 0)
        {
            Vector2 delta = Random.insideUnitCircle * sliceRadius;
            Vector3 pos = new Vector3(delta.x, 0, delta.y);
            float randomY = Random.Range(0.0f, 360.0f);
            Quaternion newQuaternion = new Quaternion();
            newQuaternion.Set(0, randomY, 0, 360.0f);
            Instantiate(slices, pos, newQuaternion);
            sliceTimer = 1.0f / timerRate;
        }
    }
    void spawnWave()
    {
        waveTimer -= Time.deltaTime;

        if (waveTimer < 0)
        {
            Vector3 floor_position = new Vector3(lowerHalf.transform.position.x, 0, lowerHalf.transform.position.z);
            Quaternion newQuaternion = new Quaternion();
            newQuaternion.Set(0, 0, 0, 360.0f);
            Instantiate(waves, floor_position, newQuaternion);
            waveTimer = 10f;
        }
    }

}
