using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class RunningGhostTrigger : MonoBehaviour
{


    /*This SC is ones player set over the trigger then it does stuff
     Link to : Ghost Trigger One */


    public GameObject Monster;
    private CameraShaker cameraShaker;

    public Demo2 demo2;


    // Start is called before the first frame update
    void Start()
    {
        cameraShaker = CameraShaker.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            demo2.EnableMic = false;
            cameraShaker.ShakeOnce(4f, 20f, 20f , 15f);
            Instantiate(Monster, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 10), new Quaternion(0 , 180, 0, 0));
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(EnableMicTrue());
        }
    }

    IEnumerator EnableMicTrue()
    {
        yield return new WaitForSeconds(12);
        demo2.EnableMic = true;
        Destroy(this.gameObject, 1f);
    }


}
