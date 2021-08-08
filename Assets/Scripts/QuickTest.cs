using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Application.isEditor)
            Time.timeScale -= 1;
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && Application.isEditor)
            Time.timeScale += 1;

    }
}
