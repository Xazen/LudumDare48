using DG.Tweening;
using UnityEngine;

public class JellyFishController : MonoBehaviour
{
    [SerializeField] 
    private float horizontalMovement;
    
    [SerializeField]
    private float verticalMovement;
    
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float delay;

    private void Start()
    {
        gameObject.transform
            .DOMove(new Vector3(horizontalMovement, verticalMovement, 0), movementSpeed)
            .SetDelay(delay)
            .SetSpeedBased(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetRelative(true)
            .SetEase(Ease.InOutQuad);
    }
}
