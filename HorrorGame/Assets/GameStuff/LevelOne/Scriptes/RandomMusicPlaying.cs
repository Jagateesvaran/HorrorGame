using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlaying : MonoBehaviour
{

    public AudioSource[] listAudioSource;
    private bool b_ChangeMusic = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (b_ChangeMusic)
        {
            b_ChangeMusic = false;
            StartCoroutine(ChangeSound(Random.Range(0, listAudioSource.Length)));
        }
    }

    IEnumerator ChangeSound(int indexMusic)
    {
        listAudioSource[indexMusic].Play();
        yield return new WaitForSeconds(5f);
        listAudioSource[indexMusic].Stop();
        yield return new WaitForSeconds(60f);
        b_ChangeMusic = true;
    }

}
