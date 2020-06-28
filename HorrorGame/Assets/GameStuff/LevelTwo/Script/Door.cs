using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public CollectibleManager collectibleManager;
    private Animation anim;


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
            if (Input.GetKeyDown("e") && collectibleManager.currentAmount == 6)
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                anim.Play("HouseDoor");
                //Debug.Log("Can Open Door");
            }

        }
      
    }
}
