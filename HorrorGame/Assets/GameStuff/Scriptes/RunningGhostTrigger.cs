using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGhostTrigger : MonoBehaviour
{
    public GameObject Monster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(Monster, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 10), new Quaternion(0 , 180, 0, 0));
            Destroy(this.gameObject);
        }
    }
}
