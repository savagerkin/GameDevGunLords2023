using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleTargetScript : MonoBehaviour
{

    public Material selected;
    public Material unselected;
    
    private MeshRenderer meshRenderer;
    
    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void selectTarget()
    {
        //change appearance to selected
        meshRenderer.material = selected;
    }

    public void deselectTarget()
    {
        //change appearance to default
        meshRenderer.material = unselected;
    }
    
}
