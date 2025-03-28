using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunScript : BaseGun
{
    public new const float fireRate = 30.0f;
    public override float getFireRate()
    {
        return fireRate;
    }
    
    public override void Shoot(Camera cam)
    {
        Vector3 mainDir = cam.transform.forward;
        Vector3 startPoint = cam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, cam.nearClipPlane));

        //shootRay(startPoint,mainDir,50f,1f);
    }

    public override void onHit(RaycastHit hit)
    {
        GameObject obj = hit.collider.gameObject;
        Debug.Log(obj.name);
    }
}
