using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() => Singletons.ScenesController.SwitchToMenu());
    }
}
