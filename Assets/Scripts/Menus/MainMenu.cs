using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup credits;

    private void Start()
    {
        startGameButton.onClick.AddListener(() => Singletons.ScenesController.SwitchToGame());
        exitButton.onClick.AddListener(() => Singletons.ScenesController.ExitApplication());
        creditsButton.onClick.AddListener(OpenCredits);
        backButton.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        credits.DOFade(0,
            0.5f).OnComplete(() => credits.gameObject.SetActive(false));
        
        mainMenu.gameObject.SetActive(true);
        mainMenu.DOFade(1.0f,
            0.5f);
    }

    private void OpenCredits()
    {
        mainMenu.DOFade(0,
            0.5f).OnComplete(() => mainMenu.gameObject.SetActive(false));
        
        credits.gameObject.SetActive(true);
        credits.DOFade(1.0f, 0.5f);
    }
}
