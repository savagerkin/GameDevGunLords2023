using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour 
{
    public GameObject impactEffect;
    public ParticleSystem muzzleFlash;
    public PlayerScript playerScript;
    public BaseGun currGun;
    BaseGun[] guns;
    Camera mainCam;
    public float gunTimer = 0;
    //firerates of different guns
    public float[] fireRate;
    public GameObject gun_being_held;
    public int gunID = 2;

    public PlayerAudio playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        guns = new BaseGun[]{new PistolScript(),
                             new ShotgunScript(impactEffect),
                             new MachineGunScript(),
                            };
        
        currGun = guns[1        ];
        fireRate = new float[]{1.0f,2.0f,30f};
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        gunTimer -= gunTimer < 0 ? 0 : Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Alpha1)) currGun = guns[0];
        if(Input.GetKeyDown(KeyCode.Alpha2)) currGun = guns[1];
        if(Input.GetKeyDown(KeyCode.Alpha3)) currGun = guns[2];

        
        if(Input.GetButton("Fire1") && gunTimer < 0)
        {
            playerScript.knockBack();
            print("Shoot");
            currGun.Shoot(mainCam);
            gunTimer = 1.6f ;
            gun_being_held.GetComponent<Animator>().Play("Recoil");
            muzzleFlash.Play();
            playerAudio.playGun();
        }
    }
    public float getGunTimer()
    {
        return gunTimer;
    }
    

}
