using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]  private bool lockX;
    [SerializeField]  private bool lockY;
    [SerializeField]  private bool lockZ;
    [SerializeField] private BillboardType billboardType;

    private Vector3 orginalRotation;
    public enum BillboardType{ LookAtCamera, CameraForward};

    void LateUpdate(){
        switch (billboardType){
            case BillboardType.LookAtCamera:
                transform.LookAt(Camera.main.transform.position, Vector3.up);
                break;
            case BillboardType.CameraForward:
                transform.forward =Camera.main.transform.forward;
                break;
            default:
                break;
        }
        Vector3 rotation = transform.rotation.eulerAngles;
        //If needed to lock the y rotation
        if (lockX) {
        rotation.x = orginalRotation.x;
        }
        if (lockY) {
        rotation.y = orginalRotation.y;
        }
        if (lockZ) {
        rotation.z = orginalRotation.z;
        }        
        transform.rotation = Quaternion.Euler(rotation);
    }
 

}
