using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    public GameObject directionalLight;
    private bool onoff_DL = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            onoff_DL = !onoff_DL; // toggles onoff at each click

            if (onoff_DL)
            {
                directionalLight.SetActive(true);
            }
            else
            {
                directionalLight.SetActive(false);
            }
        }

        if (Input.GetKeyDown("r"))
        {
            Application.LoadLevel(1);
        }
    }
}
