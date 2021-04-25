using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(() => Singletons.ScenesController.SwitchToGame());
        exitButton.onClick.AddListener(() => Singletons.ScenesController.ExitApplication());
    }
}
