using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunLook : MonoBehaviour
{
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
