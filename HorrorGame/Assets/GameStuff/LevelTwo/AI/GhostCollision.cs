﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostCollision : MonoBehaviour
{
    public GameObject DiePanel;
    public Animator animator;
    public bool istrigger;

    // Start is called before the first frame update
    void Start()
    {
        istrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (istrigger == true)
        {
            StartCoroutine(DieCoroutine());
           
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && istrigger == false)
        {
            Debug.Log("Ghost Hit Player");
            Destroy(other.gameObject);
            this.gameObject.GetComponentInChildren<Camera>().enabled = true;
            this.gameObject.GetComponentInChildren<AudioListener>().enabled = true;
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0;
            other.gameObject.GetComponentInChildren<Camera>().enabled = false;
            other.gameObject.GetComponentInChildren<CharacterController>().enabled = false;
            animator.SetBool("isWalking", false);
            istrigger = true;
        }
    }
    IEnumerator DieCoroutine()
    {
        // wait for 1 second
        yield return new WaitForSeconds(4.0f);
        DiePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
