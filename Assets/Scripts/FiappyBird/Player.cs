using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseController
{
    GameManager gameManager;

    Animator animator;
    Rigidbody2D _rigidbody;
    
    public bool isDead = false;
    public bool isFlap = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null )
        {
            Debug.Log("Animator Null");
        }
        if ( _rigidbody == null )
        {
            Debug.Log("Rigidbody Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                gameManager.Restart();
            }
        }
        else
        {
            Move();
        }
    }

    protected override void Move()
    {
        forwardSpeed = 3.0f;
        flapForce = 6.0f;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            velocity.y += flapForce;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp(_rigidbody.velocity.y * 10, -80, 80);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true;
        if (isDead == true)
        {
            animator.SetInteger("isDie", 1);

            gameManager.GameOver();
        }
    }
}
