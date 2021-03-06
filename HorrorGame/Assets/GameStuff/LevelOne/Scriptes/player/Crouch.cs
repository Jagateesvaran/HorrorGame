﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Crouch : MonoBehaviour
{
    CharacterController characterController;
    FirstPersonController firstPerson;
   

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        firstPerson = gameObject.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterController.height = 0.5f;
            firstPerson.m_WalkSpeed = 1.5f;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            characterController.height = 2f;
            firstPerson.m_WalkSpeed = 2;
        }
    }
}
