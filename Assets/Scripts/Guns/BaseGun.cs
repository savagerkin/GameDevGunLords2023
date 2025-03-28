using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseGun
{
    public GunScript gunScript;
    public RaycastHit hitt;
    protected float fireRate;

    protected int ammoCount;

    protected int clipSize;

    protected int currClip;
    public abstract void Shoot(Camera cam);

    public abstract void onHit(RaycastHit hit);

    public abstract float getFireRate();

    protected void shootRay(Vector3 origin, Vector3 dir,float gunRange, float damage)
    {
        RaycastHit hit;
        
        if(Physics.Raycast(origin,dir,out hit, gunRange))
        {        
            onHit(hit);
        }           
    }
}
