using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForCrawlingGhost : MonoBehaviour
{

    public GameObject[] locationArray;
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
            this.gameObject.GetComponent<BoxCollider>().enabled = false;   
           StartCoroutine(SpawnMonsterWave());
            
        }
    }

    IEnumerator SpawnMonsterWave()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(Monster, locationArray[0].transform.position, locationArray[0].transform.rotation);
            Instantiate(Monster, locationArray[1].transform.position, locationArray[0].transform.rotation);
            yield return new WaitForSeconds(4);
        }  
    }
}
