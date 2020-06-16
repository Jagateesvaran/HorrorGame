using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyTrigger : MonoBehaviour
{

    public AudioSource VoiceLineOne;
    public AudioSource VoiceLineTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit Player");
            StartCoroutine(ExampleCoroutine());
        }
    }


    IEnumerator ExampleCoroutine()
    {
        VoiceLineOne.Play();
        yield return new WaitForSeconds(1);
        VoiceLineTwo.Play();
    }

}
