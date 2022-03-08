using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillator : MonoBehaviour
{
    [SerializeField]
    float freq = 440f;
    float increment;
    float phase;
    float samplingFreq = 48000f;

    [SerializeField]
    float gain;

    void OnAudioFilterRead(float[] data, int channels) {
        increment = freq * 2f * Mathf.PI / samplingFreq;

        for (int i = 0; i < data.Length; i += channels) {
            phase += increment;
            data[i] = gain * Mathf.Sin(phase);

            if (channels == 2) {
                data[i + 1] = data[i];
            }

            if (phase > 2f * Mathf.PI) {
                phase = 0;
            }
        }
    }
}
