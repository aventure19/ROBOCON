using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Canvas cnv;
    public string scoreDirectory;
    public Button baseButton;
    public Transform content;
    private int contentCount = 0;

    // Use this for initialization
    void Start()
    {
        RectTransform rt = baseButton.GetComponent(typeof(RectTransform)) as RectTransform;

        string[] s = Directory.GetFiles(scoreDirectory);
        foreach (string score in s)
        {
            if (score.EndsWith(".txt"))
            {
                Vector3 offset = new Vector3((rt.rect.width) * contentCount, 0.0f, 0.0f);

                Button b = Instantiate<Button>(baseButton,
                    cnv.transform.position + offset,
                    Quaternion.identity,
                    content.GetChild(0));
                b.GetComponent<DataStore>().enabled = true;
                b.GetComponent<DataStore>().musicFilePath = score;

                string[] spl = score.Split('\\');
                b.GetComponentsInChildren<Text>()[0].text = spl.Last();

                contentCount++;
            }
        }
    }
}
