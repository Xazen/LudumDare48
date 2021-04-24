using System;
using UnityEngine;
using UnityTemplateProjects;

public class OctopusController : MonoBehaviour
{
    [SerializeField] 
    private OctopusView view;

    [SerializeField]
    private int health = 3;
    
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

    [SerializeField] private float damageCooldown = 3;
    
    
    private Rigidbody2D rb;
    private float currentSwimCooldown;
    private float currentDamageCooldown;
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
        Debug.Log($"{lastVelocity}");
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
        currentDamageCooldown -= Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag.Equals(Tag.Obstacle))
        {
            OnObstacleCollision(other);
        }
    }

    private void OnObstacleCollision(GameObject other)
    {
        Debug.Log("Hit obstacle");
        if (currentDamageCooldown < 0)
        {
            Debug.Log("Take damage");
            health -= 1;
            var knockBackForce = other.gameObject.GetComponent<ObstacleController>().GetKnockBack();
            rb.AddForce((transform.position - other.gameObject.transform.position).normalized * knockBackForce, ForceMode2D.Impulse);
            currentDamageCooldown = damageCooldown;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals(Tag.Obstacle))
        {
            OnObstacleCollision(other.gameObject);
        }
    }
}