using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    public AudioSource otherClip;

    // Start is called before the first frame update
    void Start()
    {
        otherClip.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
