using System;
using UnityEngine;

public class OctopusController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float swimPower;

    [SerializeField] 
    private float maxMoveSpeed;
    
    [SerializeField] 
    private float swimCooldown = 1.5f;
    
    private Rigidbody2D rb;
    private float currentSwimCooldown = 0f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 mInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (mInput.magnitude > 0)
        {
            rb.AddForce( mInput * speed);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && currentSwimCooldown <= 0)
        {
            rb.AddForce(mInput.normalized * swimPower, ForceMode2D.Impulse);
            currentSwimCooldown = swimCooldown;
        }

        if (rb.velocity.magnitude > maxMoveSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }
    }

    private void Update()
    {
        currentSwimCooldown -= Time.deltaTime;
    }
}
