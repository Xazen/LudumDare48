using System;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private OctopusController octopus;

    
    // Start is called before the first frame update
    private void Start()
    {
        pauseButton.gameObject.SetActive(false);
        pauseButton.onClick.AddListener(OnPauseClick);
        Singletons.ScenesController.OnSceneUnloaded += OnSceneUnloaded;
        Singletons.ScenesController.OnSceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        healthSlider.value = octopus.CurrentHealth;
    }

    private void OnSceneLoaded(int sceneid)
    {
        if (sceneid == ScenesController.PauseMenu)
        {
            pauseButton.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Singletons.ScenesController.OnSceneUnloaded -= OnSceneUnloaded;
        Singletons.ScenesController.OnSceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneUnloaded(int sceneid)
    {
        if (sceneid == ScenesController.PauseMenu ||
            sceneid == ScenesController.Story)
        {
            pauseButton.gameObject.SetActive(true);
        }
    }

    private void OnPauseClick()
    {
        Singletons.ScenesController.OpenPause();
        pauseButton.gameObject.SetActive(false);
    }
}
