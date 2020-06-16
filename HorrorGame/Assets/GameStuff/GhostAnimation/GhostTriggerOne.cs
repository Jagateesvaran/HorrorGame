using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTriggerOne : MonoBehaviour
{
    private Animator m_Animator;
    public AudioClip otherClip;



    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            transform.Translate(Vector3.forward * (5 * Time.deltaTime));
            Destroy(this.gameObject, 4f);
        }
    }
}
