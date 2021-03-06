﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameConfigs : MonoBehaviour
{
    #region Fields
    public byte lowestKey = 0x15;
    public byte highestKey = 0x6c;

    public float lowestKeyPosition = -13.18f;
    public float highestKeyPosition = 12.97f;

    public int KeyCollectionLength = 88;
    #endregion
   
    void Start()
    {
        KeyCollectionLength = highestKey - lowestKey + 1;
        DontDestroyOnLoad(this.gameObject);
    } 
}