using System;
using UnityEngine;
using UnityTemplateProjects;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Singletons.GameController = this;
        ShowStory();
        Singletons.ScenesController.OnSceneUnloaded += OnSceneUnloaded;
        Singletons.ScenesController.OnSceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        Singletons.ScenesController.OnSceneUnloaded -= OnSceneUnloaded;
        Singletons.ScenesController.OnSceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(int sceneid)
    {
        if (sceneid == ScenesController.Win || 
            sceneid == ScenesController.Lose)
        {
            IsGameRunning = false;
        }
    }

    public bool IsGameRunning { get; private set; }

    private void ShowStory()
    {
        Singletons.ScenesController.OpenStory();
    }

    private void OnSceneUnloaded(int sceneId)
    {
        if (sceneId == ScenesController.Story)
        {
            IsGameRunning = true;
        }
    }
}
