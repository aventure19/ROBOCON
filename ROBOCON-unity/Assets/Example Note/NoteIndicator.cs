using UnityEngine;
using MidiJack;

public class NoteIndicator : MonoBehaviour
{
    public int noteNumber;
    public float velocity_f = 0.0f;

    void Update()
    {
        transform.localScale = Vector3.one * (0.1f + MidiMaster.GetKey(noteNumber));

        velocity_f = MidiMaster.GetKey(noteNumber);

        var color = MidiMaster.GetKeyDown(noteNumber) ? Color.red : Color.white;
        GetComponent<Renderer>().material.color = color;
    }
}
