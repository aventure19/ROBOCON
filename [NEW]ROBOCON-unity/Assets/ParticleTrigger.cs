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
        ps.startSize = midi.data2 / 64.0f;
        ps.Emit (Mathf.Max(midi.data2 / 8, 3));
        //Debug.Log("aaa");
    }
}
