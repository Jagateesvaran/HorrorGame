using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    public GameObject UIInteractionCanvas;
    public Animation openDoorAnimation;
    public AudioSource doorCreakSound;

  
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIInteractionCanvas.gameObject.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                openDoorAnimation.Play();
                doorCreakSound.Play();
                gameObject.GetComponent<BoxCollider>().enabled = false;
                UIInteractionCanvas.gameObject.SetActive(false);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        UIInteractionCanvas.gameObject.SetActive(false);
    }








}
