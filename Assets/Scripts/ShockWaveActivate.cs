using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveActivate : MonoBehaviour
{
    private float timer = 8.5f;
    // Start is called before the first frame update
    private void Destroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy();
        }
    }
}
