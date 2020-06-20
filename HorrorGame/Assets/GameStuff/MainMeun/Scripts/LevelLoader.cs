using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation opertaion = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!opertaion.isDone)
        {
            float progress = Mathf.Clamp01(opertaion.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100 + "%";
            yield return null;
        }
    }
}
