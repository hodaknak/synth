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
    float volume = 0.1f;

    float gain;

    [SerializeField]
    GameObject datapoint;

    GameObject[] points;
    
    void Start() {

        points = new GameObject[100];

        for (var i = 0; i < 100; i++) {
            Instantiate(datapoint);
            points[i];
        }
    }
    void Update() 
    {
        
        
        if (Input.GetKey(KeyCode.A)) {
            gain = volume;
            freq = 108.9f;
        } else if (Input.GetKey(KeyCode.S)) {
            gain = volume;
            freq = 108.9f;
        } else {
            gain = 0f;
        }

        
    }

    void OnAudioFilterRead(float[] data, int channels) {
        //test
        increment = freq * 2f * Mathf.PI / samplingFreq;

        for (int i = 0; i < data.Length; i += channels) {
            phase += increment;
            data[i] = gain * Waves.Sin(phase);

            if (channels == 2) {
                data[i + 1] = data[i];
            }

            if (phase > 2f * Mathf.PI) {
                phase = 0;
            }
        }
        
        //visualize
        
        for (var i = 0; i < 100; i++) {
            points[i].position = new Vector2(i - 50, data[i]);
        }

        //for harrison: loop through the data[] array, each value is the y value so just graph that
    }
}

