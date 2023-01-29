using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;

    public void LoadScene(string sceneName)
    {

        if (sceneName != "MainMenu")
        {
            StartCoroutine(DelaySceneLoading(sceneName, 1.5f));
            loadingPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator DelaySceneLoading(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
