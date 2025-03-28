using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundTrigger : MonoBehaviour
{
    public GameObject alien;
    public GameObject Door;
    private bool playerEntered = false;
    public AudioSource playSound;
    public AudioSource Ambience;
    public AudioSource weirdAmbience;
    public GameObject shotgun;
    public float cooldown;
    float lastKnock;
    public MeshRenderer lowerB1;
    public MeshRenderer lowerB2;
    private bool gunFirstTime = false;
    public BoxCollider trigger;
    public AudioSource alienSound;
    public GameObject Text;
    public GameObject crosshair;
    void Start()
    {
        shotgun.SetActive(false);
        Ambience.Play();

    }
    void Update()
    {
        if(gunFirstTime == false && Input.GetButtonDown("Fire1") && playerEntered){
            lowerB1.enabled = true;
            lowerB2.enabled = true;
            crosshair.SetActive(true);
            gunFirstTime = true;
        }
        cooldown -= Time.deltaTime;
        if (playerEntered)
        {
            Text.SetActive(true);
            shotgun.SetActive(true);
            Ambience.Stop();
            playSound.Stop();

        }
        else
        {
            doorKnock();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if(playerEntered ==false){
            weirdAmbience.Play();
            alienSound.Play();
            alien.SetActive(true);
        }
        playerEntered = true;
        trigger.enabled = false;

    }
    void doorKnock()
    {
        if (cooldown < 0)
        {

            playSound.Play();
            cooldown = 10f;
        }
    }
}
