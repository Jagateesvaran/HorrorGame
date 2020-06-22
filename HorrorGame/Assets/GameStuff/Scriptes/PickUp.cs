using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform theDest;
    float throwForce = 600;
    bool isHolding = false;

    void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().freezeRotation = true;
        this.transform.position = theDest.position;
        this.transform.parent = GameObject.Find("Destination").transform;
        isHolding = true;
    }

    void OnMouseUp()
    {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        this.transform.parent = null;
        isHolding = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isHolding)
        {
            GetComponent<Rigidbody>().AddForce(theDest.transform.forward * throwForce);
            isHolding = false;
        }
    }



}
