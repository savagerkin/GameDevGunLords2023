using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float Yrot = 0;
    // Update is called once per frame
    void Update()
    {
        Yrot += -Input.GetAxisRaw("Mouse Y") * PlayerScript.rotationSpeed * Time.deltaTime;
        Yrot = Mathf.Clamp(Yrot , -90f, 90f);
        Vector3 angles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(Yrot,angles.y,angles.z);
    }
}
