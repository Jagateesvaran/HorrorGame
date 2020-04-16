using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAnimations : MonoBehaviour
{

    public int LightMode;
    public GameObject FlameLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LightMode == 0)
        {
            StartCoroutine(AnimateLight());
        }
    }

    IEnumerator AnimateLight()
    {
        LightMode = Random.Range(1, 4);
        if (LightMode == 1)
        {
            FlameLight.GetComponent<Animation>().Play("TorchAnim1");
        }
        else if (LightMode == 2)
        {
            FlameLight.GetComponent<Animation>().Play("TorchAnim2");
        }
        else if (LightMode == 3)
        {
            FlameLight.GetComponent<Animation>().Play("TorchAnim3");
        }

        yield return new WaitForSeconds(0.99f);
        LightMode = 0;
    }

}
