using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadline : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider other)
    {
        if (this.enabled)
        {
            if (other.transform.gameObject.tag == "Note" || other.transform.gameObject.tag == "Bar")
                Destroy(other.transform.gameObject); 
        }
    }
}
