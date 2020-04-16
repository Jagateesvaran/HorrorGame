using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Put This script on a Gameobject with a Audio Soucre and when u run it un mute it

public class MicControl : MonoBehaviour
{
    public float sensitivity = 100;
    public float loudness = 0;
    AudioSource _audio;

    public int loudnesslimit = 2;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;

        while (!(Microphone.GetPosition(null) > 0))
        {
            _audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;

        if (loudness > loudnesslimit)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(this.GetComponent<Rigidbody>().velocity.x, 4, 0);
        }
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }

        return a / 256;
    }
}
