using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameConfigs : MonoBehaviour {
    
    public byte lowestKey = 0x15;
    public byte highestKey = 0x6c;

    public float lowestKeyPosition = -13.18f;
    public float highestKeyPosition = 12.97f;
    
    public int KeyCollectionLength = 88;

    // Use this for initialization
    void Start () {
        KeyCollectionLength = highestKey - lowestKey + 1;
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[Serializable]
public struct KeyCollection
{
    public SpriteRenderer sr;
    public byte keyNum;
    public bool isKeyHolded;
}