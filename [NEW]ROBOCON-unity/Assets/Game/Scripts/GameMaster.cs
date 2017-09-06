using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    
    [SerializeField]
    private BeamLineFixed[] blf;

    public KeyCollection[] kc;
    
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
        foreach (KeyCollection KC in kc)
        {
            if (KC.keyNum == message.data1)
            {
                kc[i].sr.enabled = true;
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
                break;
            }
            i++;
        }
        kc[i].isKeyHolded = false;
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

}