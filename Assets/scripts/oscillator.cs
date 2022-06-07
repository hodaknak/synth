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

    List<Image> keyImages;

    [SerializeField]
    float volume = 0.1f;

    float gain;

    GameObject[] points;

    float[] globalData;

    LineRenderer line;
    [SerializeField]
    LineRenderer envLine;
    
    Vector3[] linePos;
    Vector3[] envPos;

    wave[] waveFunctions;

    [SerializeField]
    bool useEnv;

    bool useKeyboard;
    bool usePiano;
    
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

    public GameObject attackslider;
    Slider attacksliddy;
    public GameObject decayslider;
    Slider decaysliddy;
    public GameObject sustainslider;
    Slider sustainsliddy;
    public GameObject releaseslider;
    Slider releasesliddy;

    [SerializeField]
    Toggle envToggle;

    [SerializeField]
    Toggle reverbToggle;

    [SerializeField]
    Slider reverbSlider;

    [SerializeField]
    Toggle distortionToggle;

    [SerializeField]
    Slider distortionSlider;

    AudioReverbFilter reverbFilter;
    AudioDistortionFilter distortionFilter;

    public Toggle keyboardkeystoggle;
    public Toggle pianokeystoggle;

    public GameObject Keyboardkeytext;
    public GameObject Pianokeytext;

    void Start() {
        points = new GameObject[200];
        globalData = new float[200];
        linePos = new Vector3[200];
        envPos = new Vector3[5];

        waveFunctions = new wave[] {Waves.Sin, Waves.Square, Waves.Triangle, Waves.Saw};

        currentOctave = 1;
        line = gameObject.GetComponent<LineRenderer>();

        reverbFilter = GetComponent<AudioReverbFilter>();
        distortionFilter = GetComponent<AudioDistortionFilter>();

        octavesslider = octaveslider.GetComponent<Slider>();
        volumesslider = volumeslider.GetComponent<Slider>();
        attacksliddy = attackslider.GetComponent<Slider>();
        decaysliddy = decayslider.GetComponent<Slider>();
        sustainsliddy = sustainslider.GetComponent<Slider>();
        releasesliddy = releaseslider.GetComponent<Slider>();

        env = new Envelope(attacksliddy.value, decaysliddy.value, sustainsliddy.value, releasesliddy.value);

        Button btn = triwavebutton.GetComponent<Button>();
        btn.onClick.AddListener(TriOnClick);
        Button btn1 = sinwavebutton.GetComponent<Button>();
        btn1.onClick.AddListener(SinOnClick);
        Button btn2 = sawwavebutton.GetComponent<Button>();
        btn2.onClick.AddListener(SawOnClick);
        Button btn3 = squarewavebutton.GetComponent<Button>();
        btn3.onClick.AddListener(SquareOnClick);

        attacksliddy.onValueChanged.AddListener(AttackOnChange);
        decaysliddy.onValueChanged.AddListener(DecayOnChange);
        sustainsliddy.onValueChanged.AddListener(SustainOnChange);
        releasesliddy.onValueChanged.AddListener(ReleaseOnChange);

        reverbSlider.onValueChanged.AddListener(OnReverbChange);
        distortionSlider.onValueChanged.AddListener(OnDistortionChange);
        
        envToggle.onValueChanged.AddListener(OnEnvToggled);
        keyboardkeystoggle.onValueChanged.AddListener(OnKeyboardToggled);
        pianokeystoggle.onValueChanged.AddListener(OnPianoToggled);
        Keyboardkeytext.SetActive(false);
        Pianokeytext.SetActive(false);

        reverbToggle.onValueChanged.AddListener(OnReverbToggle);
        distortionToggle.onValueChanged.AddListener(OnDistortionToggle);

        keyImages = new List<Image>();
        
        for (int i = 0; i < keys.Count; i++)
        {
            keyImages.Add(keys[i].GetComponent<Image>());
        }
        
        envLine.positionCount = 5;
        envPos[0] = new Vector3(125f, -20f, 0f);
        
        UpdateGraph();
    }
    void Update() 
    {
        currentFreq = freq;
        currentOctave = (int)octavesslider.value;
        volume = volumesslider.value;
        

        if (Input.GetKeyDown(KeyCode.A)) {
            currentKey = KeyCode.A;
            currentKeyIndex = 0;

            freq = 110f;
            keyImages[0].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            currentKey = KeyCode.S;
            currentKeyIndex = 1;

            freq = 123.47f;
            keyImages[1].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            currentKey = KeyCode.D;
            currentKeyIndex = 2;
            
            freq = 130.81f;
            keyImages[2].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.F)) {
            currentKey = KeyCode.F;
            currentKeyIndex = 3;
            
            freq = 146.83f;
            keyImages[3].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.G)) {
            currentKey = KeyCode.G;
            currentKeyIndex = 4;
            
            freq = 164.81f;
            keyImages[4].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.H)) {
            currentKey = KeyCode.H;
            currentKeyIndex = 5;
            
            freq = 174.61f;
            keyImages[5].color = Color.grey;
            
            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.J)) {
            currentKey = KeyCode.J;
            currentKeyIndex = 6;
            
            freq = 196f;
            keyImages[6].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.K)) {
            currentKey = KeyCode.K;
            currentKeyIndex = 7;
            
            freq = 220f;
            keyImages[7].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.L)) {
            currentKey = KeyCode.L;
            currentKeyIndex = 8;
           
            freq = 246.94f;
            keyImages[8].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.Semicolon)) {
            currentKey = KeyCode.Semicolon;
            currentKeyIndex = 9;
            
            freq = 261.63f;
            keyImages[9].color = Color.grey;


            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.Quote)) {
            currentKey = KeyCode.Quote;
            currentKeyIndex = 10;
            
            freq = 293.67f;
            keyImages[10].color = Color.grey;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.W)) {
            currentKey = KeyCode.W;
            currentKeyIndex = 11;
            
            freq = 116.54f;
            keyImages[11].color = Color.black;
            
            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.R)) {
            currentKey = KeyCode.R;
            currentKeyIndex = 12;
            
            freq = 138.59f;
            keyImages[12].color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.T)) {
            currentKey = KeyCode.T;
            currentKeyIndex = 13;
            
            freq = 155.56f; 
            keyImages[13].color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.U)) {
            currentKey = KeyCode.U;
            currentKeyIndex = 14;
            
            freq = 185.00f;
            keyImages[14].color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.I)) {
            currentKey = KeyCode.I;
            currentKeyIndex = 15;
            
            freq = 207.65f;
            keyImages[15].color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.O)) {
            currentKey = KeyCode.O;
            currentKeyIndex = 16;
           
            freq = 233.08f;
            keyImages[16].color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.LeftBracket)) {
            currentKey = KeyCode.LeftBracket;
            currentKeyIndex = 17;
            
            freq = 277.18f;
            keyImages[17].color = Color.black;

            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyDown(KeyCode.RightBracket)) {
            currentKey = KeyCode.RightBracket;
            currentKeyIndex = 18;
            
            freq = 311.13f;
            keyImages[18].color = Color.black;
            
            if (useEnv) env.keyPressed();
            else gain = volume;
        } else if (Input.GetKeyUp(currentKey)) {
            currentKeyIndex = -1;
            
            if (useEnv) env.keyReleased();
            else gain = 0;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (i != currentKeyIndex)
                keyImages[i].color = Color.white;
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

            if (phase >= 2f * Mathf.PI) {
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

    void AttackOnChange(float v)
    {
        env.attack = v;
        UpdateGraph();
    }
    
    void DecayOnChange(float v)
    {
        env.decay = v;
        UpdateGraph();
    }
    
    void SustainOnChange(float v)
    {
        env.sustain = v;
        UpdateGraph();
    }
    
    void ReleaseOnChange(float v)
    {
        env.release = v;
        UpdateGraph();
    }

    void UpdateGraph()
    {
        envPos[1] = new Vector3(125 + env.attack * 8.3333f, 0f, 0f);
        envPos[2] = new Vector3(envPos[1].x + env.decay * 8.3333f, -20 + 20 * env.sustain, 0f);
        envPos[3] = new Vector3(envPos[2].x + 25, envPos[2].y, 0f);
        envPos[4] = new Vector3(envPos[3].x + env.release * 8.3333f, -20f, 0f);
        
        envLine.SetPositions(envPos);
    }

    void OnEnvToggled(bool on)
    {
        useEnv = on;
    }

    void OnKeyboardToggled(bool on)
    {
        Keyboardkeytext.SetActive(on);

        if (on) {
            Pianokeytext.SetActive(false);
            pianokeystoggle.isOn = false;
        }
    }
    void OnPianoToggled(bool on)
    {
        Pianokeytext.SetActive(on);

        if (on) {
            Keyboardkeytext.SetActive(false);
            keyboardkeystoggle.isOn = false;
        }
    }

    void OnReverbToggle(bool on) {
        reverbFilter.enabled = on;
    }

    void OnReverbChange(float v) {
        reverbFilter.reverbLevel = v;
    }

    void OnDistortionToggle(bool on) {
        distortionFilter.enabled = on;
    }

    void OnDistortionChange(float v) {
        distortionFilter.distortionLevel = v;
    }
}

