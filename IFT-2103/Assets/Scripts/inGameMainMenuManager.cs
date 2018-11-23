using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class inGameMainMenuManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);



        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            new WaitForSeconds(1);
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }

        new WaitForSeconds(1);
    }

    public void QuitToMainMenu()
    {
        LoadLevel(0);
    }
}
