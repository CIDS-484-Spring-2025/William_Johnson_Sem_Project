using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    private Renderer objectRenderer; 

    void Start()
    {
        objectRenderer = GetComponent<Renderer>(); 
    }

    public void ChangeMaterial(Material newMaterial)
    {
        if (objectRenderer != null && newMaterial != null)
        {
            objectRenderer.material = newMaterial;
        }
    }
}
