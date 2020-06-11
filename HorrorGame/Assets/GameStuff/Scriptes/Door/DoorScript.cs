using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animation anim;
    public KeyCardManager keyCardManager;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            if (Input.GetKeyDown("e") && keyCardManager.YellowKeyCard == true)
            {
                anim.Play("DoorOpening");
            }
        }
    }
}
