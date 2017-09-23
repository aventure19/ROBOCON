using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingTest : MonoBehaviour {

    bool flag = true;
    [SerializeField]
    float time = 0;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timing();
	}

    void Timing()
    {
        if(gameObject.transform.position.y <= 0 && flag)
        {
            flag = false;
            time = Time.timeSinceLevelLoad;
            return;
        }
    }
}
