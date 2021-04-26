using DG.Tweening;
using UnityEngine;

public class OctopusView : MonoBehaviour
{
    private readonly int up = Animator.StringToHash("Up");
    private readonly int down = Animator.StringToHash("Down");
    private readonly int left = Animator.StringToHash("Left");
    private readonly int right = Animator.StringToHash("Right");
    private readonly int hurt = Animator.StringToHash("Hurt");
    private readonly int idle = Animator.StringToHash("Idle");
    private readonly int horizontalMovement = Animator.StringToHash("HorizontalMovement");
    private readonly int verticalMovement = Animator.StringToHash("VerticalMovement");
    
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private float idleCooldown = 1f;

    private float currentIdleCooldown;
    
    private void Start()
    {
        PlayIdle();
    }

    public void PlayUp()
    {
        Debug.Log("Player Up");
        PlayMove(up);
    }

    public void PlayLeft()
    {
        Debug.Log("Player Left");
        PlayMove(left);
    }

    public void PlayRight()
    {
        Debug.Log("Player Right");
        PlayMove(right);
    }

    public void PlayDown()
    {
        Debug.Log("Player Down");
        PlayMove(down);
    }
    
    public void PlayIdle()
    {
        Debug.Log("Player Idle");
        animator.SetTrigger(idle);
    }

    public void UpdateMovementAnimation(float horizontal, float vertical)
    {
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            animator.SetFloat(horizontalMovement, horizontal);
            animator.SetFloat(verticalMovement, 0);
        }
        else
        {
            animator.SetFloat(horizontalMovement, 0);
            animator.SetFloat(verticalMovement, vertical);
        }
    }

    private void Update()
    {
        currentIdleCooldown -= Time.deltaTime;
    }

    private void PlayMove(int animationHash)
    {
        currentIdleCooldown = idleCooldown;
        animator.SetTrigger(animationHash);
    }

    public void PlayHurt(float animationDuration)
    {
        animator.SetTrigger(hurt);
        var loops = 16;
        sprite.DOFade(0.25f,
            animationDuration / loops).SetLoops(loops,
            LoopType.Yoyo);
    }
}
