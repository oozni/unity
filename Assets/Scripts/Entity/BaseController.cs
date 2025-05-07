using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    //protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer chracterRanderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected float flapForce = 0.0f; // 점프하는 힘
    protected float forwardSpeed = 0.0f; // 앞으로 나아가는 힘

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        Move();
        Ratate();
    }


    protected virtual void Move()
    {

    }
    protected virtual void Ratate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            chracterRanderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            chracterRanderer.flipX = false;
        }
    }
}
