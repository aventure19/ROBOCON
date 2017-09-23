using UnityEngine;
using System.Collections;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnNoteOn (MidiMessage midi)
    {
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
        ps.startSize = midi.data2 / 64.0f;
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
        ps.Emit (Mathf.Max(midi.data2 / 8, 3));
        //Debug.Log("aaa");
    }
}
