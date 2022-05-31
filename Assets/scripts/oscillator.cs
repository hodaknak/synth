 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oscillator : MonoBehaviour
{
    [SerializeField]
    WAVETYPE type;

    [SerializeField]
    float freq = 440f;
    public float currentFreq;
    float increment;
    float phase;
    float samplingFreq = 48000f;

    public int currentOctave;
    public int oldOctave;

    [SerializeField]
    List<GameObject> keys;

    [SerializeField]
    float volume = 0.1f;

    float gain;

    [SerializeField]
    GameObject datapoint;

    GameObject[] points;

    float[] globalData;

    LineRenderer line;
    Vector3[] linePos;

    wave[] waveFunctions;

    [SerializeField]
    bool[] keybools;
    
    delegate float wave(float t);

    public GameObject sliddy;
    Slider slider;

    void Start() {
        points = new GameObject[200];
        globalData = new float[200];
        linePos = new Vector3[200];
        keybools = new bool[18];

        waveFunctions = new wave[] {Waves.Sin, Waves.Square, Waves.Triangle, Waves.Saw};

        currentOctave = 1;
        line = gameObject.GetComponent<LineRenderer>();

        slider = sliddy.GetComponent<Slider>();
    }
    void Update() 
    {
        currentFreq = freq;
        if(Input.GetKey(KeyCode.Space))
        {
            gain = volume;
        } else {
            gain = 0f;
        }
        
        if (Input.GetKey(KeyCode.A)) {
            gain = volume;
            freq = 110f;
            keys[0].GetComponent<Image>().color = Color.grey;
        } else if (Input.GetKey(KeyCode.S)) {
            gain = volume;
            freq = 123.47f;
            keys[1].GetComponent<Image>().color = Color.grey;
        } else if(Input.GetKey(KeyCode.D)) {
            gain = volume;
            freq = 130.81f;
            keys[2].GetComponent<Image>().color = Color.grey;
        } else if(Input.GetKey(KeyCode.F)) {
            gain = volume;
            freq = 146.83f;
            keys[3].GetComponent<Image>().color = Color.grey;
        }  else if(Input.GetKey(KeyCode.G)) {
            gain = volume;
            freq = 164.81f;
            keys[4].GetComponent<Image>().color = Color.grey;
        } else if(Input.GetKey(KeyCode.H)) {
            gain = volume;
            freq = 174.61f;
            keys[5].GetComponent<Image>().color = Color.grey;
        }  else if(Input.GetKey(KeyCode.J)) {
            gain = volume;
            freq = 196f;
            keys[6].GetComponent<Image>().color = Color.grey;
        }  else if(Input.GetKey(KeyCode.K)) {
            gain = volume;
            freq = 220f;
            keys[7].GetComponent<Image>().color = Color.grey;
        } else if (Input.GetKey(KeyCode.L)) {
            gain = volume;
            freq = 246.94f;
            keys[8].GetComponent<Image>().color = Color.grey;
        } else if(Input.GetKey(KeyCode.Semicolon)) {
            gain = volume;
            freq = 261.63f;
            keys[9].GetComponent<Image>().color = Color.grey;
        } else if(Input.GetKey(KeyCode.Quote)) {
            gain = volume;
            freq = 293.67f;
            keys[10].GetComponent<Image>().color = Color.grey;
        }  else if(Input.GetKey(KeyCode.W)) {
            gain = volume;
            freq = 116.54f;
            keys[11].GetComponent<Image>().color = Color.black;
        } else if(Input.GetKey(KeyCode.R)) {
            gain = volume;
            freq = 138.59f;
            keys[12].GetComponent<Image>().color = Color.black;
        }  else if(Input.GetKey(KeyCode.T)) {
            gain = volume;
            freq = 155.56f; 
            keys[13].GetComponent<Image>().color = Color.black;
        }  else if(Input.GetKey(KeyCode.U)) {
            gain = volume;
            freq = 185.00f;
            keys[14].GetComponent<Image>().color = Color.black;
        }  else if(Input.GetKey(KeyCode.I)) {
            gain = volume;
            freq = 207.65f;
            keys[15].GetComponent<Image>().color = Color.black;
        } else if(Input.GetKey(KeyCode.O)) {
            gain = volume;
            freq = 233.08f;
            keys[16].GetComponent<Image>().color = Color.black;
        }else if(Input.GetKey(KeyCode.LeftBracket)) {
            gain = volume;
            freq = 277.18f;
            keys[17].GetComponent<Image>().color = Color.black;
        } else if(Input.GetKey(KeyCode.RightBracket)) {
            gain = volume;
            freq = 311.13f;
            keys[18].GetComponent<Image>().color = Color.black;
        }
        else {
            gain = 0f;
            for (var i = 0; i < keys.Count; i++) {
                keys[i].GetComponent<Image>().color = Color.white;
            }
        }
        currentFreq = freq * Mathf.Pow(2, currentOctave);


        
       

        //visualize
        for (int i = 0; i < 200; i++) {
            linePos[i] = new Vector3(i - 100, globalData[i] * 175, 0);
        }

        line.positionCount = 199;
        line.SetPositions(linePos);
        
        currentOctave = (int)slider.value;
    }

    

    void OnAudioFilterRead(float[] data, int channels) {
        //test
        increment = currentFreq * 2f * Mathf.PI / samplingFreq;

        for (int i = 0; i < data.Length; i += channels) {  
            phase += increment;
            data[i] = gain * waveFunctions[(int)type](phase);

            if (channels == 2) {
                data[i + 1] = data[i];
            }

            if (phase > 2f * Mathf.PI) {
                phase = 0;
            }
        }
        
        for (int i = 0; i < 200; i += channels) {
            globalData[i] = data[i];

            if (channels == 2) {
                globalData[i + 1] = data[i];
            }
        }

        //for harrison: loop through the data[] array, each value is the y value so just graph that
    }

    //knob events
    public void onVolumeChange(float value) {
        Debug.Log(value);
    }

    enum WAVETYPE {
        SIN, SQUARE, TRIANGLE, SAW
    }
}

