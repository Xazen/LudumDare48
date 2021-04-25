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

    public delegate void SceneDelegate(int sceneId);
    public event SceneDelegate OnSceneUnloaded;
    
    public event SceneDelegate OnSceneLoaded;
    
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
        OpenScene(PauseMenu, out pauseScene);
    }

    public void ClosePause()
    {
        CloseScene(pauseScene, PauseMenu);
    }

    public void OpenStory()
    {
        OpenScene(Story, out storyScene);
    }

    public void CloseStory()
    {
        CloseScene(storyScene, Story);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    private IEnumerator TriggerCallback(AsyncOperation unloadSceneAsync, int sceneId)
    {
        if (!unloadSceneAsync.isDone)
        {
            yield return null;
        }
        OnSceneUnloaded?.Invoke(sceneId);
    }

    public bool IsPauseOpened()
    {
        return pauseScene.IsValid();
    }

    private void CloseScene(Scene scene, int sceneId)
    {
        if (scene.IsValid())
        {
            AsyncOperation unloadSceneAsync = SceneManager.UnloadSceneAsync(sceneId);
            StartCoroutine(TriggerCallback(unloadSceneAsync, sceneId));
        }
    }

    private void OpenScene(int sceneId, out Scene scene)
    {
        scene = SceneManager.LoadScene(sceneId, new LoadSceneParameters(LoadSceneMode.Additive));
        OnSceneLoaded?.Invoke(sceneId);
    }
}
