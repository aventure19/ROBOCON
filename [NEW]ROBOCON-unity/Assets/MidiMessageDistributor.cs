using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// MIDIメッセージを分配するクラス
/// </summary>
public class MidiMessageDistributor : MonoBehaviour
{
    public GameObject[] targets;
    MidiReceiver receiver;

    void Start()
    {
        receiver = FindObjectOfType(typeof(MidiReceiver)) as MidiReceiver;
    }

    void Update()
    {
        while (!receiver.IsEmpty)
        {

            // receiverからメッセージを取得
            var message = receiver.PopMessage();

            #region 打鍵処理

            // ステータスが90(Note On)なら
            if (message.status == 0x90)
            {
                foreach (var go in targets)
                {
                    go.SendMessage("OnNoteOn", message, SendMessageOptions.DontRequireReceiver);
                }
            }

            // ステータスが80,または90&data2が00(Note Off)なら
            if (message.status == 0x80 || (message.status == 0x90 && message.data2 == 0x00))
            {
                foreach (var go in targets)
                {
                    go.SendMessage("OnNoteOff", message, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        #endregion
    }
}
