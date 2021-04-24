using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private float knockBack;

    public float GetKnockBack()
    {
        return knockBack;
    }
}
