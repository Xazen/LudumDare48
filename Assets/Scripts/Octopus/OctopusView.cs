using System;
using UnityEngine;

public class OctopusView : MonoBehaviour
{
    private readonly int up = Animator.StringToHash("Up");
    private readonly int down = Animator.StringToHash("Down");
    private readonly int left = Animator.StringToHash("Left");
    private readonly int right = Animator.StringToHash("Right");
    private readonly int idle = Animator.StringToHash("Idle");
    
    [SerializeField]
    private Animator animator;

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
        if (currentIdleCooldown <= 0)
        {
            Debug.Log("Player Idle");
            animator.SetTrigger(idle);
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
}
