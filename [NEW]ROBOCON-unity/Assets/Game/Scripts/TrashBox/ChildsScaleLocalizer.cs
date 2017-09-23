using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildsScaleLocalizer : MonoBehaviour
{

    public float defaulDistance;
    public Transform[] childs;
    [SerializeField]
    Slider sl;

    public bool flag = false;

    // Update is called once per frame
    void Update()
    {
        if (flag)
            ScaleUpdate();
    }

    void FirstValuesSet()
    {
        sl = GameObject.FindWithTag("PosSet").GetComponent<Slider>();
        childs = GetComponentsInChildren<Transform>();

        defaulDistance = Vector3.Distance(gameObject.transform.position, transform.GetChild(0).transform.position);
    }

    void ScaleUpdate()
    {
        foreach (Transform C in childs)
        {
            C.position = new Vector3(
                C.position.x * sl.value * defaulDistance,
                C.position.y * sl.value * defaulDistance,
                C.position.z * sl.value * defaulDistance
                );
        }
    }
}
