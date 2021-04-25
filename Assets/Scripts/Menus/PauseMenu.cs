using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        continueButton.onClick.AddListener(() => Singletons.ScenesController.ClosePause());    
        mainMenuButton.onClick.AddListener(() => Singletons.ScenesController.SwitchToMenu());    
    }
}
