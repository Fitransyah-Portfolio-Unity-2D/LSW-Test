using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyEvent : MonoBehaviour
{
    public event Action OnSpacePressed;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OnSpacePressed();
        }
    }
}
