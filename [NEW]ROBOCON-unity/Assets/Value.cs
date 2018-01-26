using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{
    private enum Dir
    {
        left,
        right
    }
    [SerializeField]
    private Dir dir;
    [SerializeField]
    private float mag = 0.05f;

    void Update()
    {
        switch (dir)
        {
            case Dir.left:
                transform.Translate(new Vector3(-1.0f * mag, 0.0f, 0.0f));
                break;
            case Dir.right:
                transform.Translate(new Vector3(1.0f * mag, 0.0f, 0.0f));
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Note":
                switch (dir)
                {
                    case Dir.left:
                        transform.Translate(new Vector3(1.0f * mag, 0.0f, 0.0f));
                        break;
                    case Dir.right:
                        transform.Translate(new Vector3(-1.0f * mag, 0.0f, 0.0f));
                        break;
                    default:
                        break;
                }
                Destroy(this);
                break;
            default:
                break;
        }
    }

}
