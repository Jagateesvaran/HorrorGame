using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleManager : MonoBehaviour
{

    private int Goal = 6;
    public int currentAmount = 0;

    public TextMeshProUGUI GoalUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmount == Goal)
        {
            GoalUI.text = "Escape Through the front door";
        }
    }

    public void UpdateGoalUI(int i)
    {
        currentAmount += i;
       
        GoalUI.text = "Doll:" + currentAmount.ToString() + "/ 6";
    }

 

}
