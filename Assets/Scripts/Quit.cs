using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
  
    void Update()
    {
        QuitApp();
    }

    void QuitApp()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
    }
}
