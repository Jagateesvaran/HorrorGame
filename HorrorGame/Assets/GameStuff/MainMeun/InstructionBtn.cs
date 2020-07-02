using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstructionBtn : MonoBehaviour
{
    public GameObject InstructionPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInstruction()
    {
        InstructionPanel.SetActive(true);
    }

    public void OffInstruction()
    {
        InstructionPanel.SetActive(false);
    }




}
