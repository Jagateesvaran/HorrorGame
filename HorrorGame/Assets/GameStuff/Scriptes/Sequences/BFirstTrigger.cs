using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class BFirstTrigger : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject OpeningText;
    public GameObject TheMarker;

    private void OnTriggerEnter(Collider other)
    {
        ThePlayer.GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine(ScenePlayer());
    }

    IEnumerator ScenePlayer()
    {
        OpeningText.GetComponent<TextMeshProUGUI>().text = "Looks like a weapon on that table";
        yield return new WaitForSeconds(2.5f);
        OpeningText.GetComponent<TextMeshProUGUI>().text = "";
        ThePlayer.GetComponent<FirstPersonController>().enabled = true;
        TheMarker.SetActive(true);
    }
}
