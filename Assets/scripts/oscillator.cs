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

    public int octave;

    [SerializeField]
    float volume = 0.1f;

    float gain;

    [SerializeField]
    GameObject datapoint;

    GameObject[] points;
    
    void Start() {

       
    }
    void Update() 
    {
        if(Input.GetKey(KeyCode.Space))
        {
            gain = volume;
        } else {
            gain = 0f;
        }
        
        if (Input.GetKey(KeyCode.A)) {
            gain = volume;
            freq = 110f;
        } else if (Input.GetKey(KeyCode.S)) {
            gain = volume;
            freq = 123.5f;
        } else if(Input.GetKey(KeyCode.D)) {
            gain = volume;
            freq = 130.8f;
        } else if(Input.GetKey(KeyCode.F)) {
            gain = volume;
            freq = 146.8f;
        }  else if(Input.GetKey(KeyCode.G)) {
            gain = volume;
            freq = 164.8f;
        } else if(Input.GetKey(KeyCode.H)) {
            gain = volume;
            freq = 174.6f;
        }  else if(Input.GetKey(KeyCode.J)) {
            gain = volume;
            freq = 196f;
        }  else if(Input.GetKey(KeyCode.K)) {
            gain = volume;
            freq = 220f;
        }
        else {
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
        
       

        //for harrison: loop through the data[] array, each value is the y value so just graph that
    }
}

