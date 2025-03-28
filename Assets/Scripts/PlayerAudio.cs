using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource stepSource;
    public AudioSource gunSource;
    public AudioClip[] steps;
    public AudioClip[] guns;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playGun()
    {
        gunSource.clip = guns[Random.Range(0,guns.Length)];
        gunSource.Play();
    }
    public void playStep()
    {
        stepSource.clip = steps[Random.Range(0,steps.Length)];
        stepSource.Play();
    }
}
