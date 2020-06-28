using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectibleTrigger : MonoBehaviour
{

    public CollectibleManager collectibleManager;
    public GameObject threeD_text;

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
            threeD_text.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            threeD_text.SetActive(false);

        }
    }

    private void OnDestroy()
    {
        collectibleManager.UpdateGoalUI(1);

    }
}
