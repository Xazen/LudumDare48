using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Vector3 lastPlayerConvertedWorldPoint;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);         
        pos.x = Mathf.Clamp01(pos.x);         
        pos.y = Mathf.Clamp01(pos.y);
        Vector3 playerConvertedWorldPoint = Camera.main.ViewportToWorldPoint(pos);
        transform.position = new Vector3(playerConvertedWorldPoint.x, playerConvertedWorldPoint.y, gameObject.transform.position.z);
    }

    private void Update() {
        if (Camera.main == null)
        {
            return;
        }
        
        Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);         
        pos.x = Mathf.Clamp01(pos.x);         
        pos.y = Mathf.Clamp01(pos.y);
        Vector3 playerConvertedWorldPoint = Camera.main.ViewportToWorldPoint(pos);
        transform.position = new Vector3(playerConvertedWorldPoint.x, playerConvertedWorldPoint.y, gameObject.transform.position.z);
        
        
        // Buggy camera with bounds
        // Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);         
        // pos.x = Mathf.Clamp01(pos.x);         
        // pos.y = Mathf.Clamp01(pos.y);
        // Vector3 playerConvertedWorldPoint = Camera.main.ViewportToWorldPoint(pos);
        // Vector3 playerConvertedWorldPointDiff = playerConvertedWorldPoint - lastPlayerConvertedWorldPoint;
        // if (pos.x < 0.4f || pos.x > 0.6f || pos.y < 0.4f || pos.y > 0.6f)
        // {
        //     transform.Translate(playerConvertedWorldPointDiff);
        // }
        // lastPlayerConvertedWorldPoint = playerConvertedWorldPoint;
    }
}
