﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

    public Transform target;

    void Start()
    {
        transform.LookAt(target);
        //transform.Rotate(180, 0, 0);
        //transform.localScale = new Vector3(-1, -1, 1);
    }
}
