using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicImage : MonoBehaviour
{
    public GameObject MicPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayMic());
    }
    // every 2 seconds perform the print()
    private IEnumerator DisplayMic()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            MicPanel.SetActive(false);
        }
    }
}
