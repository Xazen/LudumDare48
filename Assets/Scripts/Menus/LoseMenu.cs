using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        tryAgainButton.onClick.AddListener(() => Singletons.ScenesController.SwitchToGame());
        mainMenuButton.onClick.AddListener(() => Singletons.ScenesController.SwitchToMenu());
    }
}
