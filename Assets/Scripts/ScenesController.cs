using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTemplateProjects;

public class ScenesController : MonoBehaviour
{
    public const int MainMenu = 0;
    public const int Game = 1;
    public const int PauseMenu = 2;
    public const int Story = 3;
    public const int Boot = 3;
    
    private Scene pauseScene;
    private Scene storyScene;

    public Action<int> OnSceneUnloaded;
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        Singletons.ScenesController = this;
        SwitchToMenu();
    }

    public void SwitchToGame()
    {
        SceneManager.LoadScene(Game);
    }
    
    public void SwitchToMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void OpenPause()
    {
        pauseScene = SceneManager.LoadScene(PauseMenu, new LoadSceneParameters(LoadSceneMode.Additive));
    }

    public void ClosePause()
    {
        if (pauseScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(PauseMenu);
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void OpenStory()
    {
        storyScene = SceneManager.LoadScene(Story, new LoadSceneParameters(LoadSceneMode.Additive));
    }
    
    public void CloseStory()
    {
        if (storyScene.IsValid())
        {
            AsyncOperation unloadSceneAsync = SceneManager.UnloadSceneAsync(storyScene);
            StartCoroutine(TriggerCallback(unloadSceneAsync, Story));
        }
    }

    private IEnumerator TriggerCallback(AsyncOperation unloadSceneAsync, int sceneId)
    {
        if (!unloadSceneAsync.isDone)
        {
            yield return null;
        }
        OnSceneUnloaded?.Invoke(sceneId);
    }
}
