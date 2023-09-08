using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Global class instance to be accessed from anywhere in the project
/// </summary>
[ExecuteAlways]
public class Globals : MonoBehaviour
{
    /// <summary>
    /// Initial canvas object
    /// </summary>
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