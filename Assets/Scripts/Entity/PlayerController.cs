using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    private bool sceneChange = false;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Move()
    {
        forwardSpeed = 4f;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2 (horizontal, vertical).normalized;

        transform.position += (Vector3)movementDirection * forwardSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        sceneChange = true;
        if (sceneChange == true)
        {
            
            SceneManager.LoadScene("FiappyBird");
            sceneChange = false;
        }
        
    }
}
