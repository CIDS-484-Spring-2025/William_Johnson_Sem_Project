using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToCloseGame : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Escape")){
            Application.Quit();
        }
    }
}
