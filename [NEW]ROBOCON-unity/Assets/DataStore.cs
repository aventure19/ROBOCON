using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataStore : MonoBehaviour
{
    public string musicFilePath = null;
    public bool isReverse = false;
    public SpeedType st = SpeedType.Default;

    // Use this for initialization
    void Start()
    {
        switch (gameObject.tag)
        {
            case "DataStore":
                DontDestroyOnLoad(gameObject);
                break;
            default:
                break;
        }
        //DataStore[] ds = FindObjectsOfType(typeof(DataStore)) as DataStore[];
        //foreach (DataStore d in ds)
        //{
        //    if (d.GetInstanceID() != this.GetInstanceID())
        //    {
        //        Debug.Log("distinct!");
        //        if (d.tag == "DataStore")
        //        {
        //            Destroy(d.gameObject);
        //            Debug.Log("name " + d.gameObject.name);
        //        }
        //    }

        //}

        //GameObject[] go = GameObject.FindGameObjectsWithTag("DataStore");
        //foreach (GameObject Go in go)
        //{
        //    Debug.Log(Go.name);
        //}
        //foreach (GameObject Go in go)
        //{
        //    if (Go.GetInstanceID() != this.GetInstanceID())
        //    {
        //        Debug.Log("distinct!");
        //        Destroy(Go.gameObject);
        //    }
        //    else
        //    {
        //        Debug.Log("Matched!");
        //    }
        //}
    }

    public void DataPass()
    {
        DataStore tar = GameObject.FindWithTag("DataStore").GetComponent<DataStore>();

        tar.musicFilePath = @musicFilePath;

        Util.JumpScene("Main");
    }

    public void Reverse(GameObject go)
    {
        if (!isReverse)
        {
            isReverse = true;
            go.GetComponent<Image>().color -= new Color32(160, 160, 160, 0);
        }
        else
        {
            isReverse = false;
            go.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void BPMHalf(GameObject go)
    {
        switch (st)
        {
            case SpeedType.Half:
                st = SpeedType.Default;
                go.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            default:
                st = SpeedType.Half;
                go.GetComponent<Image>().color -= new Color32(160, 160, 160, 0);
                break;
        }
    }

    public void BPMThird(GameObject go)
    {
        switch (st)
        {
            case SpeedType.Third:
                st = SpeedType.Default;
                go.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                break;
            default:
                st = SpeedType.Third;
                go.GetComponent<Image>().color -= new Color32(160, 160, 160, 0);
                break;
        }
    }

}
