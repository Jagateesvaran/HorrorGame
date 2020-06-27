using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform theDest;
    public bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        isTrigger = false;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }


    void OnMouseDown()
    {
        if (isTrigger)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<SphereCollider>().enabled = false;
            this.transform.position = theDest.position;
            this.transform.parent = GameObject.Find("Destination").transform;
        }
    }

     void OnMouseUp()
     {
        this.GetComponent<Rigidbody>().AddForce(theDest.transform.forward * 600);
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        GetComponent<SphereCollider>().enabled = true;
     }
}
