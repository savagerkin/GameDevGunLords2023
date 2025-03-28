using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public int sceneNum;
    public float sceneTimer = 3f;
    public static float f = 0;
    public static bool canSwitch = true;
    void OnTriggerEnter(Collider other){
        SceneManager.LoadScene(sceneNum);
    }

    void Update()
    {
        sceneTimer -= Time.deltaTime * f;
        if(sceneTimer < 0 && canSwitch)
        {
            SceneManager.LoadScene(6);
            canSwitch = false;
            sceneTimer = 3;
        }
    }   
}
