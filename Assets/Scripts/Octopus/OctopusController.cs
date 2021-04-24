using UnityEngine;

public class OctopusController : MonoBehaviour
{
    [SerializeField] 
    private OctopusView view;
    
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float swimPower;

    [SerializeField] 
    private float maxMoveSpeed;
    
    [SerializeField] 
    private float swimCooldown = 1.5f;
    
    [SerializeField] 
    private float speedToSwitchToIdle = 3.3f;
    
    private Rigidbody2D rb;
    private float currentSwimCooldown;
    private float lastVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 directionInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (directionInput.magnitude > 0)
        {
            rb.AddForce( directionInput * speed);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && currentSwimCooldown <= 0)
        {
            rb.AddForce(directionInput.normalized * swimPower, ForceMode2D.Impulse);
            currentSwimCooldown = swimCooldown;
            PlayAnimation(directionInput);
        }

        if (rb.velocity.magnitude > maxMoveSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }

        if (lastVelocity > speedToSwitchToIdle && rb.velocity.magnitude < speedToSwitchToIdle)
        {
            view.PlayIdle();
        }

        lastVelocity = rb.velocity.magnitude;
    }

    private void PlayAnimation(Vector3 directionInput)
    {
        if (Mathf.Abs(directionInput.x) > Mathf.Abs(directionInput.y))
        {
            if (directionInput.x > 0)
            {
                view.PlayRight();
            }
            else
            {
                view.PlayLeft();
            }
        }
        else
        {
            if (directionInput.y > 0)
            {
                view.PlayUp();
            }
            else
            {
                view.PlayDown();
            }
        }
    }

    private void Update()
    {
        currentSwimCooldown -= Time.deltaTime;
    }
}
