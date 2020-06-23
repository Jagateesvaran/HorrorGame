using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardScript : MonoBehaviour
{

    public GameObject PickupText;
    public KeyCardManager keyCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupText.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                keyCard.YellowKeyCard = true;
                Destroy(this.gameObject);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        PickupText.SetActive(false);
    }

}
