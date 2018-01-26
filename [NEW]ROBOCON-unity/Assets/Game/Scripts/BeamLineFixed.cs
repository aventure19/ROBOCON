using UnityEngine;
using System.Collections;

public class BeamLineFixed : MonoBehaviour
{
    private SpriteRenderer sr;
    public float endScaleX;
    public float endScaleY;

    public float speed;
    private int fixedFPS = 50;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Animation();
    }

    void Animation()
    {
        if (transform.localScale.x < endScaleX)
            transform.localScale += new Vector3(1.0f, 0, 0) * speed;
        if (transform.localScale.x < endScaleY)
            transform.localScale += new Vector3(0, 1.0f, 0) * speed;
    }
}
