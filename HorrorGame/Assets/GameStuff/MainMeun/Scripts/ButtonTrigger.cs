using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonTrigger : MonoBehaviour
{
    public Animator animator;
    public LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Hover", false);
        animator.SetBool("Pressed", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(1);
        levelLoader.LoadLevel("CutScene");
    }
}
