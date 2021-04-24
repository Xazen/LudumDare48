using System.Collections;
using UnityEngine;

public class VolcanoController : MonoBehaviour
{
    [SerializeField]
    private float delay;
    
    [SerializeField]
    private ParticleSystem bubbleParticle;

    private void Start()
    {
        StartCoroutine(ActiveParticleDelayed());
    }

    private IEnumerator ActiveParticleDelayed()
    {
        yield return new WaitForSeconds(delay);
        bubbleParticle.gameObject.SetActive(true);
    }
}
