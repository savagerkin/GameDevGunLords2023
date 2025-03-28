using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    Vector3 currentRotation, targetRotation, targetPosition, currentPosition, initialGunPosition; 
    public Transform cam;
    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;
    [SerializeField] float kickBackZ;
    public float snappiness, returnAmount;
    void Start()
    {
        initialGunPosition = transform.localPosition;
    }
    // Update is called once per frame
    void Update(){
        targetRotation = Vector3. Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnAmount); 
        currentRotation = Vector3. Slerp (currentRotation, targetRotation, Time.fixedDeltaTime* snappiness); 
        transform.localRotation = Quaternion.Euler(currentRotation);
        // cam.localRotation = Quaternion. Euler (currentRotation);
        back();
    }
    ﻿

    public void recoil(){
        targetPosition -= new Vector3(0,0,kickBackZ);
        targetRotation += new Vector3 (recoilX, Random. Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ)); 
        Debug.Log("REC");
    }
    ﻿

    void back(){
        targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * returnAmount); 
        currentPosition = Vector3. Lerp (currentPosition, targetPosition, Time.fixedDeltaTime * snappiness); 
        transform.localPosition = currentPosition;
    }
}
