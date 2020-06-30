using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomClockNoise : MonoBehaviour
{

    public AudioSource dongAudio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(PlayClockAudio());
    }

    private IEnumerator PlayClockAudio()
    {
        int x = Random.Range(300, 1200);
        yield return new WaitForSeconds(x);
        dongAudio.Play();

    }


}
