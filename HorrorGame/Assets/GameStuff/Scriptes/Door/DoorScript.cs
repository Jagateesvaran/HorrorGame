using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animation anim;
    public KeyCardManager keyCardManager;
    public GameObject textOpenup;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        textOpenup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            textOpenup.SetActive(true);

            if (Input.GetKeyDown("e") && keyCardManager.YellowKeyCard == true)
            {
                anim.Play("DoorOpening");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        textOpenup.SetActive(false);
    }
}
