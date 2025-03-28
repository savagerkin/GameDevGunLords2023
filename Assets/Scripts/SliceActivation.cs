using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceActivation : MonoBehaviour
{
    public ParticleSystem firstPhase;
    public ParticleSystem secondPhaseL;
    public ParticleSystem secondPhaseR;
    public GameObject UpperSlice;
    public MeshRenderer UpperMesh;
    public BoxCollider UpperCollider; 
    public float timer = 1.5f;
    private bool triggered = false;
    public AudioSource sound;
    void Update(){
        timer -= Time.deltaTime;
        if(timer<0 && !triggered){
            turnOffFirstPhase();
            triggered = true;
        }
    }
    public void turnOffFirstPhase(){
        firstPhase.Stop();
        secondPhaseL.Play();
        secondPhaseR.Play();
        sound.Play();
        UpperMesh.enabled = true;
        UpperCollider.enabled = true;
        Invoke("Destroy", 0.75f);  
    }
    public void Destroy(){
        Destroy(gameObject);
    }

}
