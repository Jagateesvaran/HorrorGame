using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{

    private AudioSource m_AudioSource;

    [SerializeField] private AudioClip m_HeartBeatSlow;


    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();

        m_AudioSource.clip = m_HeartBeatSlow;
        m_AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
