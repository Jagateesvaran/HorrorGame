using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    public GameObject UIInteractionCanvas;
    public Animation openDoorAnimation;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIInteractionCanvas.gameObject.SetActive(true);
            Debug.Log("in Room Range");


            if (Input.GetKeyDown("e"))
            {
                openDoorAnimation.Play();
            }
        }
    }




}
