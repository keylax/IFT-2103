using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Invector.CharacterController;
public class menuManager : MonoBehaviour {

    public GameObject inGameMenu;
    public GameObject loadingScreen;
    public Slider slider;
    public Text textPercentage;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        Time.timeScale = 1;

        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(2) ;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            textPercentage.text = (progress * 100).ToString() + "%"; 

            yield return null;
        }
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        vThirdPersonCamera tpCamera = FindObjectOfType<vThirdPersonCamera>();
        tpCamera.enabled = true;
        inGameMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
