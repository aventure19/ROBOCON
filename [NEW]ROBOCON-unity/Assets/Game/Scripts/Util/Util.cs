using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util : MonoBehaviour
{

    #region Public Methods
    public static long ByteArrayToLong(byte[] b)
    {
        byte[] reversedBytes = b.Reverse().ToArray();

        long value = 0;
        int Power = 0;

        foreach (byte B in reversedBytes)
        {
            value += B * (256L ^ Power);
            Power++;
        }

        return value;
    }

    public static int ByteArrayToInt(byte[] b)
    {
        byte[] reversedBytes = b.Reverse().ToArray();

        int value = 0;
        int Power = 0;

        foreach (byte B in reversedBytes)
        {
            value += B * (256 ^ Power);
            Power++;
        }

        return value;
    }

    public static int CountChar(string s, char c)
    {
        return s.Length - s.Replace(c.ToString(), "").Length;
    }

    public static int textSave(string txt)
    {
        using (StreamWriter sw = new StreamWriter(@"C:\Users\owner\Documents\UnityLog.txt", false) /*true=追記 false=上書き*/ )
        {
            sw.WriteLine(txt);
            sw.Flush();
            sw.Close();

        }
        return 0;
    }
    #endregion
}

#region Public Structs
/// <summary>
/// ゲームの設定を提供します
/// </summary>
public struct GameModes
{
    OctaveConfig oc;
    /// <summary>
    /// 白鍵盤だけにするか（trueならオリジナル譜面）
    /// </summary>
    bool WhiteOnly;
    /// <summary>
    /// ロングを含めるか（trueならオリジナル譜面）
    /// </summary>
    bool IsLongExist;
}

/// <summary>
/// キーボードの各キーの実装を提供する構造体です
/// </summary>
[Serializable]
public struct KeyCollection
{
    public SpriteRenderer sr;
    public byte keyNum;
    public bool isKeyHolded;
}

/// <summary>
/// ノーツの実装を提供する構造体です
/// </summary>
[Serializable]
public struct Note
{
    public double start;
    public double end;
    public int key;
}

/// <summary>
/// 譜面のヘッダ情報を構成する構造体です
/// </summary>
[Serializable]
public struct Score
{
    public string MusicName;// 曲の名前
    public string inst;     // 使用する楽器
    public int BPM;         // bpm
    public int cbeats;      // 分母分子。MBeats分のCbeats拍子
    public int mbeats;      // 
    public int delta;       // 全音符の分解能（何分割できるか。4分音符...delta / 4）
    public int otp;         // オリジナルトランスポーズ
    public int offset;	    // 最初の小節までの時間
    public double measure;  // (60 * (CBeats / MBewats)) / BPM
    public Note[] notes;    // ノーツ。ScoreConfigでRenewする
}

/// <summary>
/// 譜面で使用されるコンテキスト群を定義する構造体です
/// </summary>
public struct ScoreContext
{
    public string header, header_end, name, bpm, offset, mbeats, cbeats, delta, inst, otp, score, score_end;

    public ScoreContext(string Header, string HeaderEnd, string Name, string BPM,
        string Offset, string MBeats, string CBeats, string Delta, string Inst, 
        string OTP, string Score, string ScoreEnd)
    {
        header = Header;
        header_end = HeaderEnd;
        name = Name;
        bpm = BPM;
        offset = Offset;
        mbeats = MBeats;
        cbeats = CBeats;
        delta = Delta;
        inst = Inst;
        otp = OTP;
        score = Score;
        score_end = ScoreEnd;
    }
}

#endregion

#region Enums

/// <summary>
/// オクターブモードの設定
/// </summary>
public enum OctaveConfig
{
    Original,
    Three_Octaves,
    Five_Octaves
}

#endregion