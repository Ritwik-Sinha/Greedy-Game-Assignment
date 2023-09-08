using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Globals : MonoBehaviour
{
    public GameObject canvasPrefab;
    public static Globals instance;

    void Awake()
    {
        if(instance==null)
            instance = this;
    }
    void OnEnable()
    {
        if(instance==null)
            instance=this;
    }
    void Start()
    {
        
    }

    void Update()
    {
    }
}