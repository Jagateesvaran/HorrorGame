using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class Opening : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject FadeScreenIn;
    public GameObject OpeningText;

    // Start is called before the first frame update
    void Start()
    {
        ThePlayer.GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine(ScenePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   IEnumerator ScenePlayer()
   {
        yield return new WaitForSeconds(1.5f);
        FadeScreenIn.SetActive(false);
        OpeningText.GetComponent<TextMeshProUGUI>().text = "I need to get out of here";
        yield return new WaitForSeconds(2f);
        OpeningText.GetComponent<TextMeshProUGUI>().text = "";
        ThePlayer.GetComponent<FirstPersonController>().enabled = true;

    }
}
