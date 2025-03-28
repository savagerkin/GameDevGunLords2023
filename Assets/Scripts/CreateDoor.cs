using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDoor : MonoBehaviour
{
    public GameObject Text ;
    public GameObject door;
    public GameObject itself;
   void OnTriggerEnter(Collider other){
    door.SetActive(true);
    Text.SetActive(true);
    Destroy(itself);
   }

}
