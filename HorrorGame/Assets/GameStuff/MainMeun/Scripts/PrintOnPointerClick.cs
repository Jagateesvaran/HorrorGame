using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrintOnPointerClick : MonoBehaviour
{
    public Animator animator;
    public LevelLoader levelLoader;
    public GameObject SettingsPanel;
    public GameObject MainMeun;

    public void MouseHover()
    {
        animator.SetBool("Hover", true);
    }

    public void MouseExit()
    {
        animator.SetBool("Hover", false);
    }

    public void MousePressed()
    {
        animator.SetBool("Pressed", true);
        SettingsPanel.SetActive(true);
        MainMeun.SetActive(false);
    }


    public void MouseBackPressed()
    {
        animator.SetBool("Pressed", true);
        SettingsPanel.SetActive(false);
        MainMeun.SetActive(true);
    }

    public void MouseQuitPressed()
    {
        animator.SetBool("Pressed", true);
        Application.Quit();
    }
}