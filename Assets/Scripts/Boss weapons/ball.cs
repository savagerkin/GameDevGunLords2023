using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    public float initVel = 10f;
    void Start()
    {
        Destroy(gameObject,5f);
        GetComponent<Rigidbody>().AddForce(transform.forward * initVel);
    }
}
