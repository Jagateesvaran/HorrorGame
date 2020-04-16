using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{

    public float TheDistance;

    // This is for the UI FOR E PRESS
    public GameObject ActionDisplay;
    public GameObject ActionText;

    // the door object
    public GameObject TheDoorHinge;

    public AudioSource CreakSound;

    // Extra Ccrusor
    public GameObject ExtraCross;

    void Update()
    {
        TheDistance = PlayerRayCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        // near the door
        if (TheDistance <= 2)
        {
            ExtraCross.SetActive(true);
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }

        // if e is pressed and distance is 3 or less 
        if (Input.GetButtonDown("Action") && (TheDistance <= 2))
        {
            this.GetComponent<BoxCollider>().enabled = false;
            ActionDisplay.SetActive(false);
            ActionText.SetActive(false);
            TheDoorHinge.GetComponent<Animation>().Play("FirstDoorOpen");
            CreakSound.Play();
        }
    }

    void OnMouseExit()
    {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
        ExtraCross.SetActive(false);

    }

}
