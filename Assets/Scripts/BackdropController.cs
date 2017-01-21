﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropController : MonoBehaviour
{
    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z);
    }
}
