using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Vector2 targetPos;
    public float radius = 30f;
    public float velocity = 3f;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = Random.insideUnitCircle * radius;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos2D = new Vector2(transform.position.x,transform.position.z);
        if(Vector2.Distance(pos2D,targetPos) < 1)
        {
            targetPos = Random.insideUnitCircle * radius;
        }
        transform.LookAt(new Vector3(targetPos.x, transform.position.y,targetPos.y));
        Vector2 delta = (targetPos - pos2D).normalized * velocity * Time.deltaTime;
        transform.position += new Vector3(delta.x, 0, delta.y);
    }

}
