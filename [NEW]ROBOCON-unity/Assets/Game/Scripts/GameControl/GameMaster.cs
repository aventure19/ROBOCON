using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

/// <summary>
/// 演奏シーンのゲーム進行を実装します
/// </summary>
public class GameMaster : MonoBehaviour
{
    #region Fields
    public GameObject keyboardVisual;
    public Transform judgeLine;
    public Transform limitPoint;

    public KeyCollection[] kc;

    private GameConfigs gc;
    #endregion

    // Start(), Update()が含まれます
    //
    #region GameProcess


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        gc = GameObject.FindWithTag("Config").GetComponent<GameConfigs>();

        //Array.Resize(ref blf, gc.KeyCollectionLength);
        //blf = gameObject.GetComponentsInChildren<BeamLineFixed>();

        KeyAsign();
        MaskAsign();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject[] gos = new GameObject[2];
            gos[0] = GameObject.FindWithTag("Config");
            gos[1] = GameObject.FindWithTag("DataStore");
            
            Util.JumpScene("Select");
            foreach (GameObject go in gos)
                Destroy(go);
        }
    }
    #endregion


    // 打鍵処理系のメソッドが含まれます
    //
    #region Private Methods

    /// <summary>
    /// 鍵盤が押されたとき発生
    /// </summary>
    /// <param name="message"></param>
    void OnNoteOn(MidiMessage message)
    {
        int i = 0;
        foreach (KeyCollection KC in kc)
        {
            if (KC.keyNum == message.data1)
            {
                kc[i].sr.enabled = true;
                Judge(i, JudgeType.On);
                break;
            }
            i++;
        }
        kc[i].isKeyHolded = true;
    }

    /// <summary>
    /// 鍵盤が離されたとき発生
    /// </summary>
    /// <param name="message"></param>
    void OnNoteOff(MidiMessage message)
    {
        int i = 0;
        foreach (KeyCollection KC in kc)
        {
            if (KC.keyNum == message.data1)
            {
                kc[i].sr.enabled = false;
                Judge(i, JudgeType.Off);
                break;
            }
            i++;
        }
        kc[i].isKeyHolded = false;
    }

    /// <summary>
    /// 判定処理
    /// </summary>
    /// <param name="keyIndex"></param>
    /// <param name="jType"></param>
    void Judge(int keyIndex, JudgeType jType)
    {
        // 整理
        Vector3 keyPos = kc[keyIndex].sr.transform.position;
        Vector3 limPos = limitPoint.position;
        Vector3 judgePos = judgeLine.position;

        switch (jType)
        {
            case JudgeType.On:

                RaycastHit hit;
                if (Physics.Raycast(
                        new Ray(new Vector3(keyPos.x, limPos.y), Vector3.up),
                        out hit,
                        Mathf.Infinity,
                        LayerMask.GetMask(new string[] { "Note" })
                        )
                    )
                {
                    // 判定
                    try
                    {
                        if (Vector2.Distance(hit.transform.position,
                            new Vector3(keyPos.x, judgePos.y)) <
                            Vector2.Distance(judgePos, limPos))
                        {
                            Debug.Log("great");
                        }
                        else
                        {
                            Debug.Log("Poor");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Debug.Log("Target none");
                    }
                }

                #region NeedFix
                //// 判定対象
                //RaycastHit2D hit = Physics2D.Raycast(
                //    new Vector3(keyPos.x, limPos.y),
                //    Vector3.up,
                //    Mathf.Infinity,
                //    LayerMask.GetMask(new string[] { "Note" })
                //    );
                //Debug.DrawRay(
                //    new Vector3(keyPos.x, limPos.y),
                //    Vector3.up * 5,
                //    Color.red,
                //    0.5f
                //    );

                //// 判定
                //try
                //{
                //    if (Vector2.Distance(hit.transform.position,
                //        new Vector3(keyPos.x, judgePos.y)) <
                //        Vector2.Distance(judgePos, limPos))
                //    {
                //        Debug.Log("great");
                //    }
                //    else
                //    {
                //        Debug.Log("Poor");
                //    }
                //}
                //catch (NullReferenceException ex)
                //{
                //    Debug.Log("Target none");
                //}
                #endregion
                break;
            case JudgeType.Off:
                break;
            default:
                break;
        }
    }
    #endregion

    // ゲーム情報の設定を行います
    //
    #region Config Methods

    /// <summary>
    /// 鍵盤を配列に割当て
    /// </summary>
    void KeyAsign()
    {
        kc = new KeyCollection[gc.KeyCollectionLength];

        for (int i = 0; i < kc.Length; i++)
        {
            kc[i].keyNum = gc.lowestKey;

            kc[i].keyNum += (byte)i;

            kc[i].isKeyHolded = false;
        }
    }

    /// <summary>
    /// 打鍵時のマスクを割当て
    /// </summary>
    void MaskAsign()
    {
        for (int i = 0; i < kc.Length; i++)
        {
            kc[i].sr = keyboardVisual.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>();
        }

        // srを無効化
        for (int i = 0; i < gc.KeyCollectionLength; i++)
        {
            kc[i].sr.enabled = false;
        }
    }

    #endregion

    #region Private Enums
    enum JudgeType
    {
        On,
        Off,
        Hold
    }

    #endregion
}