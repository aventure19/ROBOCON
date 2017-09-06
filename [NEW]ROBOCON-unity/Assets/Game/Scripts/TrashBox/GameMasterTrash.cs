using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameMasterTrash : MonoBehaviour
{
    
    [SerializeField]
    private BeamLineFixed[] blf;
    [SerializeField]
    private SpriteRenderer[] sr;

    public byte[] keyCollection;
    public bool[] IsKeyHolded;
    
    public GameObject keyboardVisual;
    
    private GameConfigs gc;

    /// <summary>
    /// Start(), Update()が含まれます
    /// </summary>
    #region GameProcess

    // Use this for initialization
    void Start()
    {
        gc = GameObject.FindWithTag("Config").GetComponent<GameConfigs>();

        //Array.Resize(ref blf, gc.KeyCollectionLength);
        //blf = gameObject.GetComponentsInChildren<BeamLineFixed>();

        KeyAsign();
        MaskAsign();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion


    /// <summary>
    /// 打鍵処理系のメソッドが含まれます
    /// </summary>
    #region Private Methods

    /// <summary>
    /// 鍵盤が押されたとき発生
    /// </summary>
    /// <param name="message"></param>
    void OnNoteOn(MidiMessage message)
    {
        int i = 0;
        foreach (byte b in keyCollection)
        {
            if (b == message.data1)
            {
                sr[i].enabled = true;
            }
            i++;
        }
    }

    /// <summary>
    /// 鍵盤が離されたとき発生
    /// </summary>
    /// <param name="message"></param>
    void OnNoteOff(MidiMessage message)
    {
        int i = 0;
        foreach (byte b in keyCollection)
        {
            if (b == message.data1)
            {
                sr[i].enabled = false;
            }
            i++;
        }
    }
    #endregion

    /// <summary>
    /// ゲーム情報の設定を行います
    /// </summary>
    #region Configs

    /// <summary>
    /// 鍵盤を配列に割当て
    /// </summary>
    void KeyAsign()
    {
        Array.Resize(ref keyCollection, gc.KeyCollectionLength);
        Array.Resize(ref IsKeyHolded, gc.KeyCollectionLength);

        for (int i = 0; i < keyCollection.Length; i++)
        {
            keyCollection[i] = gc.lowestKey;

            keyCollection[i] += (byte)i;

            IsKeyHolded[i] = false;
        }
    }

    /// <summary>
    /// 打鍵時のマスクを割当て
    /// </summary>
    void MaskAsign()
    {
        Array.Resize(ref sr, gc.KeyCollectionLength);
        sr = keyboardVisual.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i <= gc.KeyCollectionLength; i++)
        {
            if (i != 0) sr[i - 1] = sr[i];
        }

        // srを無効化
        for (int i = 0; i < gc.KeyCollectionLength; i++)
        {
            sr[i].enabled = false;
        }
    }

    #endregion

}
