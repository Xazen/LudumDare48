using Unity.VisualScripting;
using UnityEngine;
using UnityTemplateProjects;

public class OctopusController : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] 
    private OctopusView view;

    [Header("Sfx")]
    [SerializeField] 
    private AudioSource swimSfx;

    [SerializeField]
    private AudioSource bumpSfx;
    
    [SerializeField]
    private AudioSource hurtSfx;

    [Header("Balancing - Movement")]
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

    [SerializeField]
    private float terrainKnockBack = 10;
    
    [Header("Balancing - Health")]
    [SerializeField]
    private int health = 3;

    [SerializeField]
    private float damageCooldown = 3;
    
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
        if (Singletons.GameController != null && !Singletons.GameController.IsGameRunning)
        {
            return;
        }
        
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
            swimSfx.Play();
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
        if (Singletons.GameController == null || !Singletons.GameController.IsGameRunning)
        {
            return;
        }
        
        currentSwimCooldown -= Time.deltaTime;
        currentDamageCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && !Singletons.ScenesController.IsPauseOpened())
        {
            Singletons.ScenesController.OpenPause();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag.Equals(Tag.Obstacle))
        {
            OnObstacleCollision(other);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case Tag.Obstacle:
                OnObstacleCollision(other.gameObject);
                return;
            case Tag.Terrain:
                OnTerrainCollision(other);
                return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(Tag.Goal))
        {
            Singletons.ScenesController.OpenWin();
        }
    }

    private void KnockBack(GameObject other, float force)
    {
        rb.AddForce((transform.position - other.gameObject.transform.position).normalized * force,
            ForceMode2D.Impulse);
    }

    private void OnObstacleCollision(GameObject other)
    {
        if (currentDamageCooldown < 0)
        {
            hurtSfx.Play();
            health -= 1;
            KnockBack(other, other.gameObject.GetComponent<ObstacleController>().GetKnockBack());
            currentDamageCooldown = damageCooldown;
            view.PlayHurt(damageCooldown);
            Singletons.ScenesController.OpenLose();
        }
    }

    private void OnTerrainCollision(Collision2D other)
    {
        // bumpSfx.Play();
        KnockBack(other.gameObject, terrainKnockBack);
    }
}