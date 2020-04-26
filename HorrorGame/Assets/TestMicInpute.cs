using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMicInpute : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {


        if (MicInput.MicLoudness >= 1.2E-06 && MicInput.MicLoudness <= 7.3E-07) // whisper
        {
            Debug.Log("Whispering");
        }
        else if (MicInput.MicLoudness >= 1.0E-06)
        {
            Debug.Log("Shouting");
        }
        else
        {
            Debug.Log("NOt whispering and Shouting");
        }

    }
}
