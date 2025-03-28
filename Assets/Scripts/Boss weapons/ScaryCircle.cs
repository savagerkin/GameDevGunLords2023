using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryCircle : MonoBehaviour
{
    private float timer = 2.5f;
    public GameObject damage;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            explode();
        }
    }

    private void explode()
    {
        //spawn explosion i guess
        Destroy(Instantiate(damage,transform.position,Quaternion.identity),1.5f);
        Destroy(gameObject);
    }
}
