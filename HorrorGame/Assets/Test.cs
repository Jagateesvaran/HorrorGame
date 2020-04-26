using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float db = MicInput.MicLoudnessinDecibels;

        if (db < 1 && db > -20f)
        {
            Debug.Log("Move");
            //InputGameManager.instance.ItsOKtoMove();
        }

        Debug.Log("Volume is " + MicInput.MicLoudness.ToString("##.#####") + ", decibels is :" + MicInput.MicLoudnessinDecibels.ToString("######"));
        //Debug.Log("Volume is " + NormalizedLinearValue(MicInput.MicLoudness).ToString("#.####") + ", decibels is :" + NormalizedDecibelValue(MicInput.MicLoudnessinDecibels).ToString("#.####"));
    }


    float NormalizedLinearValue(float v)
    {
        float f = Mathf.InverseLerp(.000001f, .001f, v);
        return f;
    }

    float NormalizedDecibelValue(float v)
    {
        float f = Mathf.InverseLerp(-114.0f, 6f, v);
        return f;
    }
}
