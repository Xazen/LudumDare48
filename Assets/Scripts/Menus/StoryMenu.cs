using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class StoryMenu : MonoBehaviour
{
    [SerializeField]
    private Image background;
    
    [SerializeField]
    private TMP_Text storyText;

    [SerializeField]
    private AudioSource charSfx;
    private bool isTextCompleted;
    private TweenerCore<string, string, StringOptions> textTweener;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayText());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && textTweener != null)
        {
            if (isTextCompleted == false)
            {
                textTweener?.Complete(true);
            }
            else
            {
                Singletons.ScenesController.CloseStory();
            }
        }
    }

    private IEnumerator PlayText()
    {
        Color bgmColor = background.color;
        bgmColor.a = 0;
        background.color = bgmColor;
        background.DOFade(1f, 1f);
        
        var text = storyText.text;
        storyText.text = string.Empty;
        yield return new WaitForSeconds(1.5f);
        var charAnimationDuration = 0.13f;
        isTextCompleted = false;
        textTweener = storyText.DOText(text, text.Length * charAnimationDuration)
            .OnComplete(() => isTextCompleted = true);

        for (int i = 0; i < text.Length; i++)
        {
            charSfx.pitch = Random.Range(0.8f,
                1.2f);
            charSfx.Play();
            yield return new WaitForSeconds(charAnimationDuration);
            if (isTextCompleted)
            {
                break;
            }
        }
    }
}