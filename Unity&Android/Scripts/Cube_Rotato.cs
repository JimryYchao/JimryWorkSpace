﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Rotato : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime*50f);
    }
}
