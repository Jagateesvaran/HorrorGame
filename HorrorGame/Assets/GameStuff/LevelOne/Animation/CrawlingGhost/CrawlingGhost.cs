using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingGhost : MonoBehaviour
{

    public float movementSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.forward * Time.deltaTime * movementSpeed;
    }

    IEnumerator ExampleCoroutine()
    {

        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }


}
