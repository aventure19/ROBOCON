using UnityEngine;
using System.Collections;

public class BeamLine : MonoBehaviour
{

    public float MaxLength = 1.0f;
    public float StartSize = 1.0f;
    public float AnimationSpd = 0.1f;
    public Color BeamColor = Color.white;

    private float NowAnm;
    private LineRenderer line;

    private float NowLength;

    private bool bStop;

    public float GetNowLength()
    {
        return NowLength;
    }
    public void StopLength(float length)
    {
        NowLength = length;
        bStop = true;
    }
    void LineFunc()
    {
        if (!bStop)
            NowLength = Mathf.Lerp(0, MaxLength, NowAnm);
        float width = Mathf.Lerp(StartSize, 0, NowAnm);
        #pragma warning disable CS0618 // 型またはメンバーが古い形式です
        line.SetWidth(width, width);
        #pragma warning restore CS0618 // 型またはメンバーが古い形式です
        float length = NowLength;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + (transform.forward * length));
    }

    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
        line.SetColors(BeamColor, BeamColor);
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
        NowAnm = 0;
        NowLength = 0;
        bStop = false;
        LineFunc();
    }

    // Update is called once per frame
    void Update()
    {

        NowAnm += AnimationSpd;

        if (NowAnm > 1.0)
        {
            Destroy(this.gameObject);
        }
        LineFunc();
    }
}
