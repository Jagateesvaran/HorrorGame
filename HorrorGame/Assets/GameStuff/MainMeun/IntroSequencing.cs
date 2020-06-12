﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroSequencing : MonoBehaviour
{

    public GameObject textBox;
    public GameObject dateDisplay;
    public GameObject placeDisplay;
    public AudioSource line01;
    public AudioSource line02;
    public AudioSource line03;
    public AudioSource line04;
    public AudioSource line05;
    public AudioSource thudSound;
    public GameObject allBlack;
    public GameObject loadText;

    public GameObject FadeOut;

    void Start()
    {
        StartCoroutine(SequenceBegin());
    }

    IEnumerator SequenceBegin()
    {
        yield return new WaitForSeconds(3);
        placeDisplay.SetActive(true);
        yield return new WaitForSeconds(1);
        dateDisplay.SetActive(true);
        yield return new WaitForSeconds(4);
        placeDisplay.SetActive(false);
        dateDisplay.SetActive(false);
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TextMeshProUGUI>().text = "The night of October 29th 2008 changed me forever.";
        line01.Play();
        yield return new WaitForSeconds(4.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "I headed out to investigate the haunting sounds in the woods.";
        line02.Play();
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(7);
        textBox.GetComponent<TextMeshProUGUI>().text = "I stumbled upon a clearing with a cabin in the distance.";
        line03.Play();
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "I could hear those sounds again coming from there.";
        line04.Play();
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(6);
        textBox.GetComponent<TextMeshProUGUI>().text = "Little did I know that this was only the beginning.";
        line05.Play();
        yield return new WaitForSeconds(11);
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        thudSound.Play();
        SceneManager.LoadScene(2);
    }




}