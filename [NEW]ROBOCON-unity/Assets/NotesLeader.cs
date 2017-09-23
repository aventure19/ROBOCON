using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesLeader : MonoBehaviour {

    private ScoreConfig sc;

	// Use this for initialization
	void Start () {
        sc = GameObject.FindWithTag("Config").GetComponent<ScoreConfig>();
	}
	
	// Update is called once per frame
	void Update () {
        PositionUpdate();
	}

    void PositionUpdate()
    {
        transform.position += Vector3.down * Time.deltaTime * sc.HS;
    }
}
