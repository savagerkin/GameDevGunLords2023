using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : BaseGun
{
    public int damage = 7;
    public new const float fireRate = 5.0f;
 
    GameObject impactEffect;

    public ShotgunScript(GameObject impactEffect){
        this.impactEffect = impactEffect;
    }
    public override float getFireRate()
    {
        return fireRate;
    }
    public override void Shoot(Camera cam)
    {
        //first make the shotgun since thats easy-ish
        // use random spread for now, but use normal distribution
        int numBullets = 15; //number of bullets on an axis
        Vector3 mainDir = cam.transform.forward;
        Vector3 startPoint = cam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, cam.nearClipPlane));
        for (int i = 0; i < numBullets; i++)
        {
            //animation
            float u1 = Random.value;
            float u2 = Random.value;
            
            //rotation angles
            float dx = Mathf.Sqrt(-2*Mathf.Log(u1))*Mathf.Cos(2*Mathf.PI * u2);
            float dy = Mathf.Sqrt(-2*Mathf.Log(u1))*Mathf.Sin(2*Mathf.PI * u2);

            Quaternion rotation = Quaternion.Euler(dy,dx,0);
            Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, rotation,Vector3.one);

            Vector3 newDir = m.MultiplyVector(mainDir);
            shootRay(startPoint,newDir,50f,1f);
            
        }
    }



    public override void onHit(RaycastHit hit)
    {
        GameObject obj = hit.collider.gameObject;
        GameObject.Destroy(GameObject.Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)), 0.5f);
        Debug.Log(obj.gameObject.tag);
        if(obj.gameObject.CompareTag("Enemy")){
            Enemy enemy = obj.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(damage);
        }
    }    

}
