using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0618 // 型またはメンバーが古い形式です
public class CameraSetting : MonoBehaviour
{
    public Canvas targetCanvas;

    public float distance = 0.0f;
    public Vector3 center = Vector3.zero;
    public Slider angle_Unit;
    public float mag = 0.1f;
    public float zoomMag = 0.2f;

    private Camera cam;
    

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        distance = Vector3.Distance(gameObject.transform.position, center);

        if (GameObject.FindWithTag("DataStore").GetComponent<DataStore>().isReverse)
            AngleReverse();
    }

    // Update is called once per frame
    void Update()
    {
        //PosSet();
        CanvasAvail();
        Scaling();
    }

    /// <summary>
    /// 試作
    /// </summary>
    void AngleSet()
    {
        gameObject.transform.Rotate(gameObject.transform.position, angle_Unit.value);
    }

    /// <summary>
    /// 反転
    /// </summary>
    public void AngleReverse()
    {
        Quaternion q = transform.rotation;
        transform.Rotate(Vector3.forward * 180);
        targetCanvas.transform.gameObject.transform.GetChild(0).transform.Rotate(Vector3.forward * 180);
    }

    /// <summary>
    /// 円運動をもとに位置調整
    /// </summary>
    void PosSet()
    {
        transform.RotateAround(center, Vector3.up, 1.0f);
    }

    /// <summary>
    /// UIを有効化
    /// </summary>
    void CanvasAvail()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            targetCanvas.enabled = true;
        }

        if (targetCanvas.enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            targetCanvas.enabled = false;
        }
    }

    void Scaling()
    {
        if (Input.GetKey(KeyCode.Keypad2))
            transform.Translate(Vector3.down * mag);
        if (Input.GetKey(KeyCode.Keypad8))
            transform.Translate(Vector3.up * mag);
        if (Input.GetKey(KeyCode.Keypad4))
            transform.Translate(Vector3.left * mag);
        if (Input.GetKey(KeyCode.Keypad6))
            transform.Translate(Vector3.right * mag);
        if (Input.GetKey(KeyCode.Keypad7))
            cam.fieldOfView += zoomMag;
        if (Input.GetKey(KeyCode.Keypad9))
            cam.fieldOfView -= zoomMag;
    }
}
