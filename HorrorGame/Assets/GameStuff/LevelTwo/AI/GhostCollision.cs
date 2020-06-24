using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour
{
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
            Debug.Log("Ghost Hit Player");
            this.gameObject.GetComponentInChildren<Camera>().enabled = true;
            other.gameObject.GetComponentInChildren<Camera>().enabled = false;
        }
    }

}
