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
    }

    private void OnDestroy()
    {
        Singletons.ScenesController.OnSceneUnloaded -= OnSceneUnloaded;
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
