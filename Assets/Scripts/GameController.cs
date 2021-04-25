using UnityEngine;
using UnityTemplateProjects;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Singletons.GameController = this;
        ShowStory();
    }
    
    public bool IsGameRunning { get; private set; }

    private void ShowStory()
    {
        Singletons.ScenesController.OpenStory();
        Singletons.ScenesController.OnSceneUnloaded = OnSceneUnloaded;
    }

    private void OnSceneUnloaded(int sceneId)
    {
        if (sceneId == ScenesController.Story)
        {
            IsGameRunning = true;
        }
    }
}
