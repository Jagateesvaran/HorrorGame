using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TryAgain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart_Level()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void LoadMainMeun()
    {
        SceneManager.LoadScene("MainMeun");
    }
}
