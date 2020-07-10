using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerPrefab : MonoBehaviour
{
    // Slider for Mic Sen
    public TextMeshProUGUI sliderTMP;
    public Slider slider;

    // DropDown For Color for Scan
    public TMP_Dropdown dropDownTMP;

    // Game Music
    public AudioMixer audioMixer;
    public Slider audioSlider;

    // Start is called before the first frame update
    void Start()
    {     
        sliderTMP.text = PlayerPrefs.GetFloat("MicSen", 1).ToString();
        slider.value = PlayerPrefs.GetFloat("MicSen");
        dropDownTMP.value = PlayerPrefs.GetInt("ScanColor", 0);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("MusicVolume", -80));
        audioSlider.value = PlayerPrefs.GetFloat("MusicVolume", -80);
    }

    // Update is called once per frame
    void Update()
    {
        sliderTMP.text = PlayerPrefs.GetFloat("MicSen").ToString();
        slider.value = PlayerPrefs.GetFloat("MicSen");
        dropDownTMP.value = PlayerPrefs.GetInt("ScanColor");
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("MusicVolume"));
        audioSlider.value = PlayerPrefs.GetFloat("MusicVolume", -80);

    }






    public void DropDownInput(int val)
    {
        PlayerPrefs.SetInt("ScanColor", val);
    }

    public void sliderUpdate(float value)
    {
        sliderTMP.text = value.ToString();
        PlayerPrefs.SetFloat("MicSen", value);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);

    }
}
