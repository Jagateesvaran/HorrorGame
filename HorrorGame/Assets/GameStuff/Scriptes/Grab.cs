using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{


    RaycastHit hit;
    GameObject grabbedOBJ;
    public Transform grabPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(transform.position, hit.point, Color.red);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(transform.position, transform.forward, out hit, 20) && hit.transform.GetComponent<Rigidbody>())
        {
            grabbedOBJ = hit.transform.gameObject;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            grabbedOBJ = null;
        }

        if (grabbedOBJ)
        {
            grabbedOBJ.GetComponent<Rigidbody>().velocity = 10 * ( grabPos.position - grabbedOBJ.transform.position);
        }
    }
}
