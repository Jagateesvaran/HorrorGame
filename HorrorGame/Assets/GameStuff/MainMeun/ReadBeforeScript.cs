using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBeforeScript : MonoBehaviour
{


    public GameObject ReadBeforePanel;
    public GameObject MainMeunPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        ReadBeforePanel.SetActive(true);
        yield return new WaitForSeconds(5);
        ReadBeforePanel.SetActive(false);
        MainMeunPanel.SetActive(true);
    }
}
