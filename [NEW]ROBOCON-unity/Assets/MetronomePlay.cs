using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomePlay : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        GameObject Metro = GameObject.FindGameObjectWithTag("Metronome");
        AudioSource aus = Metro.GetComponent<AudioSource>();
        AudioClip[] ac = Metro.GetComponent<Clips>().ac;

        switch (other.tag)
        {
            case "Measure_M":
                aus.clip = ac[0];
                aus.Play();
                break;
            case "Measure_B":
                aus.clip = ac[1];
                aus.Play();
                break;
            default:
                break;
        }
    }
}
