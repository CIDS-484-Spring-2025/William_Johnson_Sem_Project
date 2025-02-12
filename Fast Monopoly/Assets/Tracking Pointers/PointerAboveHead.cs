using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PointerAboveHead : MonoBehaviour
{
    public PointerArrowPrefabBehavior pointerPrefab;
    public string displayText;  // Use 'string' instead of 'String'
    [HideInInspector] public GameObject instantiatedPointer;

    void Start()
    {
        instantiatedPointer = Instantiate(pointerPrefab.gameObject, References.canvas.pointersChild);
        pointerPrefab.displayText.text = displayText;
    }

    void Update()
    {
        instantiatedPointer.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

}