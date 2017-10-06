using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesLeader : MonoBehaviour
{

    private ScoreConfig scf;

    // Use this for initialization
    void Start()
    {
        scf = GameObject.FindWithTag("Config").GetComponent<ScoreConfig>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PositionUpdate();
    }

    public void PositionUpdate(int dir = 1)
    {
        transform.Translate(Vector3.down * Time.deltaTime * scf.HS * dir);
    }
}
