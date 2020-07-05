using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPrefab : MonoBehaviour
{
    public TextMeshProUGUI sliderTMP;
    public TMP_Dropdown dropDownTMP;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("MicSen", 1f);
        PlayerPrefs.SetInt("ScanColor", 0);

    }

    // Update is called once per frame
    void Update()
    {
        
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
}
