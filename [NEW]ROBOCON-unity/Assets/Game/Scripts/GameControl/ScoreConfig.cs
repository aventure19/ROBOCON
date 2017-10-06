using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;
using System.IO;
using System.Text;


/// <summary>
/// ノーツの生成等を提供します
/// </summary>
public class ScoreConfig : MonoBehaviour
{

    #region Fields
    [HideInInspector]
    public string[] AvailableExtension = { "mid", "midi", "smf" };

    public float HS = 1.0f;

    [SerializeField]
    Score s;
    [SerializeField]
    ScoreContext sc = new ScoreContext("*HEADER*", "*HEADER_END*", "NAME=", "BPM=",
                "OFFSET=", "MBEATS=", "CBEATS=", "DELTA=", "INST=", "OTP=", "*SCORE*", "*SCORE_END*");

    public GameObject receiver;

    public GameObject leader;
    public GameObject note;

    public string fpath = @"C:\Users\owner\Documents\SMFTest\Canon_Score2.txt";
    //"C:\\Users\\owner\\Documents\\SMFTest\\Canon_Score.txt";

    float sinceStart = 0.0f;
    [SerializeField]
    float sinceHead = 0.0f;
    bool flag = true;
    bool sceneFlag = false;

    #endregion

    // Start(), Update()が含まれます
    //
    #region GameProcess
    // Use this for initialization
    void Start()
    {
        receiver.SendMessage("ScoreConfigProcess", fpath);

        receiver.SendMessage("ScoreInstantiate");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        OffsetAjust(ref flag);

        GameProcess();
    }
    #endregion
    
    #region Private Methods

    #region Fields

    #endregion

    /// <summary>
    /// 専用テキストより譜面生成
    /// </summary>
    /// <param name="fs"></param>
    /// <returns></returns>
    int ScoreConfigProcess(string fpath)
    {
        // ファイル読み込み
        using (FileStream fs = new FileStream(
            fpath,
            FileMode.Open,
            FileAccess.Read))
        {
            #region Asignments
            s = new Score();
            sc = new ScoreContext("*HEADER*", "*HEADER_END*", "NAME=", "BPM=",
                "OFFSET=", "MBEATS=", "CBEATS=", "DELTA=", "INST=", "OTP=", "*SCORE*", "*SCORE_END*");
            // sr.ReadLine()の結果を保持する文字列バッファ
            string buffer = "default";

            #endregion

            #region Inside Processes
            try
            {
                // fsからリーダーを構成
                using (StreamReader sr = new StreamReader(fs))
                {
                    // ゲームで使用できないファイルは除外
                    if (sr.ReadToEnd().IndexOf(sc.header) < 0)
                    {
                        throw new Exception("Cant use file.\n" + fs.Name);
                    }

                    // テキスト先頭に移動
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);

                    while (sr.Peek() > -1)
                    {
                        buffer = sr.ReadLine();
                        #region HEADER
                        // エントリー
                        if (buffer.IndexOf(sc.header) >= 0)
                        {
                            while (buffer.IndexOf(sc.header_end) < 0)
                            {
                                #region HEADER_Inside
                                buffer = sr.ReadLine();

                                //if (buffer.IndexOf(sc.name) >= 0)
                                //    s.MusicName = buffer.Trim(sc.name.ToCharArray()).Trim();

                                // NAMEまで読み飛ばし、代入
                                while (buffer.IndexOf(sc.name) < 0)
                                    buffer = sr.ReadLine();
                                s.MusicName = buffer.Trim(sc.name.ToCharArray()).Trim();

                                // BPMまで読み飛ばし、代入
                                while (buffer.IndexOf(sc.bpm) < 0)
                                    buffer = sr.ReadLine();
                                s.BPM = int.Parse(buffer.Trim(sc.bpm.ToCharArray()).Trim());

                                // OFFSETまで読み飛ばし、代入
                                while (buffer.IndexOf(sc.offset) < 0)
                                    buffer = sr.ReadLine();
                                s.offset = int.Parse(buffer.Trim(sc.offset.ToCharArray()).Trim());

                                // MBEATSまで読み飛ばし、代入
                                while (buffer.IndexOf(sc.mbeats) < 0)
                                    buffer = sr.ReadLine();
                                s.mbeats = int.Parse(buffer.Trim(sc.mbeats.ToCharArray()).Trim());

                                // CBEATSまで読み飛ばし、代入
                                while (buffer.IndexOf(sc.cbeats) < 0)
                                    buffer = sr.ReadLine();
                                s.cbeats = int.Parse(buffer.Trim(sc.cbeats.ToCharArray()).Trim());

                                // DELTAまで読み飛ばし、代入
                                while (buffer.IndexOf(sc.delta) < 0)
                                    buffer = sr.ReadLine();
                                s.delta = int.Parse(buffer.Trim(sc.delta.ToCharArray()).Trim());

                                // INSTまで読み飛ばし、代入
                                while (buffer.IndexOf(sc.inst) < 0)
                                    buffer = sr.ReadLine();
                                s.inst = buffer.Trim(sc.inst.ToCharArray()).Trim();

                                // OTPまで読み飛ばし、代入
                                while (buffer.IndexOf(sc.otp) < 0)
                                    buffer = sr.ReadLine();
                                s.otp = int.Parse(buffer.Trim(sc.otp.ToCharArray()).Trim());

                                // ヘッダ終了後、スコープから抜ける
                                break;
                                #endregion

                            }
                        }
                        #endregion

                        #region Score
                        // エントリー
                        if (buffer.IndexOf(sc.score) >= 0)
                        {
                            #region Asignments
                            string[] data;
                            int notesCount = 0;
                            int measureCount = 0;
                            double measureSeconds = (240 / (double)s.BPM) * (s.cbeats / s.mbeats);
                            // scoreBuffer.Append(sr.ReadToEnd());
                            // s.notes = new Note[Util.CountChar(sr.ReadToEnd(), '|')];

                            s.notes = new Note[Util.CountString(sr.ReadToEnd(), "ON:")];
                            sr.BaseStream.Seek(0, SeekOrigin.Begin);
                            s.se = new ScoreEffect[Util.CountStrings(sr.ReadToEnd(), "END:")];
                            sr.BaseStream.Seek(0, SeekOrigin.Begin);
                            #endregion

                            #region Score_Inside
                            // スコア開始まで読み飛ばし
                            while (true)
                            {
                                buffer = sr.ReadLine();
                                if (buffer.IndexOf(sc.score) >= 0)
                                    break;
                            }

                            // 解析ルーチン
                            while (true)
                            {
                                buffer = sr.ReadLine();

                                // ノートデータを見つけた
                                if (buffer.IndexOf(":") >= 0)
                                {
                                    data = buffer.Split(':');

                                    // 頭のデータで条件分岐
                                    switch (data[0])
                                    {
                                        case "ON":
                                            // キーを取得
                                            int scale = 0;
                                            int octave = int.Parse(data[1].Substring(1)) * 12;
                                            switch (data[1].Substring(0, 1))
                                            {
                                                case "A":
                                                    scale = 9;
                                                    break;
                                                case "B":
                                                    scale = 11;
                                                    break;
                                                case "C":
                                                    scale = 0;
                                                    break;
                                                case "D":
                                                    scale = 2;
                                                    break;
                                                case "E":
                                                    scale = 4;
                                                    break;
                                                case "F":
                                                    scale = 5;
                                                    break;
                                                case "G":
                                                    scale = 7;
                                                    break;
                                                default:
                                                    break;
                                            }
                                            s.notes[notesCount].key = scale + octave;

                                            // スタートタイム、エンドタイム取得、計算
                                            double startTime = double.Parse(data[2].Split(' ')[0]);
                                            double endTime = double.Parse(data[2].Split(' ')[1]);
                                            s.notes[notesCount].start = (240 / (double)s.BPM) * (startTime / s.delta) + (measureCount - 1) * measureSeconds;
                                            s.notes[notesCount].end = (240 / (double)s.BPM) * (endTime / s.delta) + (measureCount - 1) * measureSeconds;

                                            notesCount++;
                                            break;

                                        case "END":
                                            Debug.Log("hoge");
                                            double dummyTiming = double.Parse(data[1]);
                                            double Timing = (240 / (double)s.BPM) * (dummyTiming / s.delta) + (measureCount - 1) * measureSeconds;
                                            s.se[0].et = EffectType.End;
                                            s.se[0].timing = (float)Timing + (float)s.offset / 1000;
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                if (buffer.IndexOf("/M") >= 0)
                                    measureCount++;

                                // スコア終了後、ループから抜ける
                                if (buffer.IndexOf(sc.score_end) >= 0)
                                    break;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    //sr.Dispose();
                }
            }
            // 例外
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                //fs.Dispose();
                //System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true); //第二引数のtrueがファイルや行番号をキャプチャするため必要
                //foreach (var frame in trace.GetFrames())
                //{
                //    Debug.Log(frame.GetFileName());     //filename
                //    Debug.Log(frame.GetFileLineNumber());   //line number
                //    Debug.Log(frame.GetFileColumnNumber());  //column number
                //}
            }
            #endregion
        }

        return 0;
    }

    /// <summary>
    /// ScoreConfigProcess後、譜面オブジェクトを生成
    /// </summary>
    /// <returns></returns>
    int ScoreInstantiate()
    {
        Transform JudgeLine = GameObject.FindWithTag("Judge").transform;
        Transform Keyboard = GameObject.FindWithTag("Keyboard").transform;
        GameObject leaderP = Instantiate(
            leader,
            JudgeLine.transform.position/*new Vector3(0, ((float)s.offset / 1000) * HS, 0)*/,
            Quaternion.identity);

        for (int i = 0; i < s.notes.Length; i++)
        {
            float pos = Keyboard.GetChild(s.notes[i].key + (4 - 1)).transform.position.x;
            Vector3 v = new Vector3(pos,
                leaderP.transform.position.y + (float)s.notes[i].start,
                0.0f);
            GameObject noteP = Instantiate(note, Vector3.zero, Quaternion.identity);
            noteP.transform.position = v;
            noteP.transform.SetParent(leaderP.transform, true);

            noteP.transform.localScale = new Vector3(
                noteP.transform.localScale.x,
                noteP.transform.lossyScale.y * (float)(s.notes[i].end - s.notes[i].start),
                noteP.transform.localScale.z
                );
        }

        leaderP.transform.localScale = new Vector3(
            leaderP.transform.localScale.x,
            leaderP.transform.localScale.y * HS,
            leaderP.transform.localScale.z
            );

        //leaderP.SendMessage("FirstValuesSet");
        //leaderP.GetComponent<ChildsScaleLocalizer>().flag = true;

        return 0;
    }

    /// <summary>
    /// オフセットに合わせて譜面の位置を調整
    /// </summary>
    /// <param name="flag"></param>
    void OffsetAjust(ref bool flag)
    {
        GameObject leader = GameObject.FindWithTag("Leader");
        if (flag)
        {
            if (sinceStart >= (float)s.offset / 1000)
            {
                leader.GetComponent<NotesLeader>().enabled = true;
                flag = false;
                sceneFlag = true;
                return;
            }

            leader.transform.Translate(Vector3.up * Time.deltaTime * HS);

            sinceStart += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// 演奏中の各種効果や打鍵時の判定処理を行います
    /// </summary>
    void GameProcess()
    {
        if (sceneFlag)
        {
            int i = 0;
            
            sinceHead += Time.fixedDeltaTime;
            
            if(sinceHead >= s.se[i].timing)
            {
                switch (s.se[i].et)
                {
                    case EffectType.End:
                        Util.JumpScene("Result");
                        sceneFlag = false;
                        break;
                }

                i++;
            }
        }
    }
    #endregion
}