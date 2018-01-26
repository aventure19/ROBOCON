using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTest : MonoBehaviour
{

    DataStore ds;
    float f = 1.0f;

    // Use this for initialization
    void Start()
    {
        ds = GameObject.FindWithTag("DataStore").GetComponent<DataStore>();
        switch (ds.st)
        {
            case SpeedType.Default:
                f = 1.0f;
                break;
            case SpeedType.Half:
                f = 2.0f;
                break;
            case SpeedType.Third:
                f = 3.0f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LoopProt();
    }

    void LoopProt()
    {
        if (transform.position.y < -290.0f * f || Input.GetKeyDown(KeyCode.Space))
            SetPos();

    }

    void SetPos()
    {
        transform.position = new Vector3(
            transform.position.x,
            5.0f,
            transform.position.z
            );
    }
}
