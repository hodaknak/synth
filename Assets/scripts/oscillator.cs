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

    GameObject[] points;

    float[] globalData;

    LineRenderer line;
    Vector3[] linePos;

    wave[] waveFunctions;

    [SerializeField]
    bool useEnv;
    
    delegate float wave(float t);

    public GameObject octaveslider;
    Slider octavesslider;
    public GameObject volumeslider;
    Slider volumesslider;

    Envelope env;

    public Button triwavebutton;
    public Button sinwavebutton;
    public Button sawwavebutton;
    public Button squarewavebutton;

    KeyCode currentKey;
    int currentKeyIndex;

    public float attackvalue;
    public float decayvalue;
    public float sustainvalue;
    public float releasevalue;

    public GameObject attackslider;
    Slider attacksliddy;
    public GameObject decayslider;
    Slider decaysliddy;
    public GameObject sustainslider;
    Slider sustainsliddy;
    public GameObject releaseslider;
    Slider releasesliddy;

    void Start() {
        points = new GameObject[200];
        globalData = new float[200];
        linePos = new Vector3[200];

        waveFunctions = new wave[] {Waves.Sin, Waves.Square, Waves.Triangle, Waves.Saw};

        currentOctave = 1;
        line = gameObject.GetComponent<LineRenderer>();

        octavesslider = octaveslider.GetComponent<Slider>();
        volumesslider = volumeslider.GetComponent<Slider>();
        attacksliddy = attackslider.GetComponent<Slider>();
        decaysliddy = decayslider.GetComponent<Slider>();
        sustainsliddy = sustainslider.GetComponent<Slider>();
        releasesliddy = releaseslider.GetComponent<Slider>();

        env = new Envelope(attackvalue, decayvalue, sustainvalue, releasevalue);
        useEnv = true;

        Button btn = triwavebutton.GetComponent<Button>();
        btn.onClick.AddListener(TriOnClick);
        Button btn1 = sinwavebutton.GetComponent<Button>();
        btn1.onClick.AddListener(SinOnClick);
        Button btn2 = sawwavebutton.GetComponent<Button>();
        btn2.onClick.AddListener(SawOnClick);
        Button btn3 = squarewavebutton.GetComponent<Button>();
        btn3.onClick.AddListener(SquareOnClick);
    }
    void Update() 
    {
        currentFreq = freq;
        currentOctave = (int)octavesslider.value;
        volume = volumesslider.value;
        attackvalue = attacksliddy.value;
        decayvalue = decaysliddy.value;
        sustainvalue = sustainsliddy.value;
        releasevalue = releasesliddy.value;

        if (Input.GetKeyDown(KeyCode.A)) {
            currentKey = KeyCode.A;
            currentKeyIndex = 0;

            freq = 110f;
            keys[0].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            currentKey = KeyCode.S;
            currentKeyIndex = 1;

            freq = 123.47f;
            keys[1].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            currentKey = KeyCode.D;
            currentKeyIndex = 2;
            
            freq = 130.81f;
            keys[2].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.F)) {
            currentKey = KeyCode.F;
            currentKeyIndex = 3;
            
            freq = 146.83f;
            keys[3].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.G)) {
            currentKey = KeyCode.G;
            currentKeyIndex = 4;
            
            freq = 164.81f;
            keys[4].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.H)) {
            currentKey = KeyCode.H;
            currentKeyIndex = 5;
            
            freq = 174.61f;
            keys[5].GetComponent<Image>().color = Color.grey;
            
            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.J)) {
            currentKey = KeyCode.J;
            currentKeyIndex = 6;
            
            freq = 196f;
            keys[6].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.K)) {
            currentKey = KeyCode.K;
            currentKeyIndex = 7;
            
            freq = 220f;
            keys[7].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.L)) {
            currentKey = KeyCode.L;
            currentKeyIndex = 8;
           
            freq = 246.94f;
            keys[8].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.Semicolon)) {
            currentKey = KeyCode.Semicolon;
            currentKeyIndex = 9;
            
            freq = 261.63f;
            keys[9].GetComponent<Image>().color = Color.grey;


            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.Quote)) {
            currentKey = KeyCode.Quote;
            currentKeyIndex = 10;
            
            freq = 293.67f;
            keys[10].GetComponent<Image>().color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.W)) {
            currentKey = KeyCode.W;
            currentKeyIndex = 11;
            
            freq = 116.54f;
            keys[11].GetComponent<Image>().color = Color.black;
            
            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.R)) {
            currentKey = KeyCode.R;
            currentKeyIndex = 12;
            
            freq = 138.59f;
            keys[12].GetComponent<Image>().color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.T)) {
            currentKey = KeyCode.T;
            currentKeyIndex = 13;
            
            freq = 155.56f; 
            keys[13].GetComponent<Image>().color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.U)) {
            currentKey = KeyCode.U;
            currentKeyIndex = 14;
            
            freq = 185.00f;
            keys[14].GetComponent<Image>().color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.I)) {
            currentKey = KeyCode.I;
            currentKeyIndex = 15;
            
            freq = 207.65f;
            keys[15].GetComponent<Image>().color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.O)) {
            currentKey = KeyCode.O;
            currentKeyIndex = 16;
           
            freq = 233.08f;
            keys[16].GetComponent<Image>().color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.LeftBracket)) {
            currentKey = KeyCode.LeftBracket;
            currentKeyIndex = 17;
            
            freq = 277.18f;
            keys[17].GetComponent<Image>().color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyDown(KeyCode.RightBracket)) {
            currentKey = KeyCode.RightBracket;
            currentKeyIndex = 18;
            
            freq = 311.13f;
            keys[18].GetComponent<Image>().color = Color.black;
            
            if (useEnv) env.keyPressed();
            else gain = volume;;
        } else if (Input.GetKeyUp(currentKey)) {
            currentKeyIndex = -1;
            
            if (useEnv) env.keyReleased();
            else gain = 0;
        }

            for (var i = 0; i < keys.Count; i++)
            {
                if (i != currentKeyIndex)
                    keys[i].GetComponent<Image>().color = Color.white;
            }

        if (useEnv)
            gain = volume * env.getAmp(Time.time);

        currentFreq = freq * Mathf.Pow(2, currentOctave);

        //visualize
        for (int i = 0; i < 200; i++) {
            linePos[i] = new Vector3(i - 100, globalData[i] * 175, 0);
        }

        line.positionCount = 199;
        line.SetPositions(linePos);
        
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

    }

    //knob events
    public void onVolumeChange(float value) {
        Debug.Log(value);
    }

    enum WAVETYPE {
        SIN, SQUARE, TRIANGLE, SAW
    }


    void TriOnClick()
    {
        type = WAVETYPE.TRIANGLE;

    }
    void SinOnClick()
    {
        type = WAVETYPE.SIN;

    }
    void SawOnClick()
    {
        type = WAVETYPE.SAW;

    }
    void SquareOnClick()
    {
        type = WAVETYPE.SQUARE;

    }

}

